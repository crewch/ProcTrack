namespace AuthService.Models
{
    public class RightHoldMapper: BaseEntity
    {
        public int? HoldId { get; set; }
        public Hold? Hold { get; set; }
        public int? RightId { get; set; }
        public Right? Right { get; set; }
    }
}
