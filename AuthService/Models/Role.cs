namespace AuthService.Models
{
    public class Role: BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Right> Rights { get; set; }
        public ICollection<RightRoleMapper> RightRole { get; set; } = new List<RightRoleMapper>();
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<UserRoleMapper> UserRoles { get; set; } = new List<UserRoleMapper>();

    }
}
