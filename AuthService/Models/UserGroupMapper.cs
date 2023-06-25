namespace AuthService.Models
{
    public class UserGroupMapper : BaseEntity
    {
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public bool? IsBoss { get; set; }
    }
}
