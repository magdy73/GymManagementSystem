namespace GymManagementSystem.DTO
{
    public class AttendanceDTO
    {
        public int AttendanceId { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string CheckInTime { get; set; }
    }

}
