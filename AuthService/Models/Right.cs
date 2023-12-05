namespace AuthService.Models
{
    public class Right : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Hold> Holds { get; set; } = new List<Hold>();
        public ICollection<RightStatusMapper> RightStatus = new List<RightStatusMapper>();
        public ICollection<Status> Statuses { get; set; } = new List<Status>();
        
        public ICollection<RightRoleMapper> RightRole = new List<RightRoleMapper>();
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
