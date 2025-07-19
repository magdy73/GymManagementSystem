using GymManagementSystem.Data;
using GymManagementSystem.DataModels;
using GymManagementSystem.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymManagementSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext context;
        private readonly IConfiguration config;

        public AuthService(AppDbContext context,IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            var user = context.user.FirstOrDefault(a=>
                a.UserName== loginDTO.UserName &&a.Password==loginDTO.Password);
            if (user == null) return null;
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token=new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);



        }

        public async Task<bool> Register(RegisterDTO registerDTO)
        {
            if (await context.user.AnyAsync(u => u.UserName == registerDTO.UserName))
                return false;
            var NewUser = new UserModel
            {
                UserName = registerDTO.UserName,
                Password = registerDTO.Password,
                Role = registerDTO.Role

            };
            context.Add(NewUser);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
