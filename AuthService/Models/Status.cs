namespace AuthService.Models
{
    public class Status : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Right> Rights { get; set; }
        public ICollection<RightStatusMapper> RightStatus { get; set; } = new List<RightStatusMapper>();
        public ICollection<UserHoldMapper> UserStatus { get; set; } = new List<UserHoldMapper>();
        public ICollection<GroupHoldMapper> GroupHoldMember { get; set; } = new List<GroupHoldMapper>();
        public ICollection<GroupHoldMapper> GroupHoldBoss { get; set; } = new List<GroupHoldMapper>();
    }
}
