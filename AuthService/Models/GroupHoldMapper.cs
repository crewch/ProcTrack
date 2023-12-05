namespace AuthService.Models
{
    public class GroupHoldMapper: BaseEntity
    {
        public int? HoldId { get; set; }
        public Hold? Hold { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public int? StatusMemberId { get; set; }
        public Status? StatusMember { get; set; }
        public int? StatusBossId { get; set; }
        public Status? StatusBoss { get; set; }

    }
}
