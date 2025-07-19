using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymManagementSystem.DataModels
{
    public class AttendanceModel
    {
        public int Id { get; set; }

        [ForeignKey("Member")]
        [Required]
        public int MemberId { get; set; }
        
        public virtual MemberModel? Member { get; set; }

        public DateTime CheckInTime { get; set; } = DateTime.Now;
    }
}
