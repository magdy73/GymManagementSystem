using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GymManagementSystem.DataModels
{
    public class PaymentModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [ForeignKey("Member")]
        [Required]
        public int memberId { get; set; }
        [JsonIgnore]
        public virtual MemberModel? Member { get; set; }
        public int Amount{ get; set; }
        public int DurationByMonths { get; set; }
        [JsonIgnore]
        public DateTime PaymentDate { get; set; }= DateTime.Now;
        public string? PaymentMethod { get; set; }
        public string? Notes { get; set; }
    }
}
