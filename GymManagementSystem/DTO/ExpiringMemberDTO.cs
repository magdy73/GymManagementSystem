namespace GymManagementSystem.DTO
{
    public class ExpiringMemberDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly MembershipEndDate { get; set; }
        public string Status { get; set; }
    }
}
