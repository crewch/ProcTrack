namespace AuthService.Models
{
    public class Role: BaseEntity
    {
        public string Title { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<UserRoleMapper> UserRoles { get; set; } = new List<UserRoleMapper>();
    }
}
