using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DataModels
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
