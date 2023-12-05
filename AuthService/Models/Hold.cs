namespace AuthService.Models
{
    public class Hold: BaseEntity
    {
        public int? TypeId { get; set; }
        public int DestId { get; set; }
        public Type? Type { get; set; }
        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<GroupHoldMapper> GroupHolds { get; set; } = new List<GroupHoldMapper>();
        public ICollection<UserHoldMapper> UserHolds { get; set; } = new List<UserHoldMapper>();
    }
}
