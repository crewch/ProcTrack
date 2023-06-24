namespace AuthService.Models
{
    public class Group: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Hold> Holds { get; set; } = new List<Hold>();
        public ICollection<GroupHoldMapper> GroupHolds { get; set;} = new List<GroupHoldMapper>();
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<UserGroupMapper> UserGroups { get; set; } = new List<UserGroupMapper>();
    }
}
