using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GymManagementSystem.DataModels
{
    public class MemberModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        // Membership details
        public DateOnly MembershipStartDate { get; set; }
        public DateOnly MembershipEndDate { get; set; }

        [ForeignKey("AssignedTrainer")]
        public int TrainerId { get; set; }
        [JsonIgnore]
        public virtual TrainerModel? AssignedTrainer { get; set; }

    }
}
