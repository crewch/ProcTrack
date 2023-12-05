namespace AuthService.Models
{
    public class GroupHoldMapper: BaseEntity
    {
        public int? HoldId { get; set; }
        public Hold? Hold { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
    }
}
