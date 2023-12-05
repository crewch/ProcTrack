namespace AuthService.Models
{
    public class RightStatusMapper : BaseEntity
    {
        public int? RightId { get; set; }
        public Right? Right { get; set; }
        public int? StatusId { get; set; }
        public Status? Status { get; set; }
    }
}
