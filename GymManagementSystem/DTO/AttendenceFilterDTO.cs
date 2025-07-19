namespace GymManagementSystem.DTO
{
    public class AttendenceFilterDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "CheckInTime";
        public bool SortDescending {  get; set; }=false;

    }
}
