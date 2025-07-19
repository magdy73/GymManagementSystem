namespace GymManagementSystem.DTO
{
    public class PaymentDTO
    {
        public int Id {  get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string PaymentDate {  get; set; }
        public string EndDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Notes { get; set; }
    }
}
