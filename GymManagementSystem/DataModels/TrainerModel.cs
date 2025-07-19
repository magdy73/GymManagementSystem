using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymManagementSystem.DataModels
{
    public class TrainerModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public string? Schedule { get; set; }
        [JsonIgnore]
        public ICollection<MemberModel> Members { get; set; } = new List<MemberModel>();

    }
}
