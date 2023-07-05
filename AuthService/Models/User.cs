using Newtonsoft.Json;

namespace AuthService.Models
{
    public class User: BaseEntity
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        
        public ICollection<Hold> Holds { get; set; } = new List<Hold>();
        public ICollection<UserHoldMapper> UserHolds{ get; set; } = new List<UserHoldMapper>();
        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<UserGroupMapper> UserGroups { get; set; } = new List<UserGroupMapper>();
        public ICollection<Role> Roles { get; set; } = new List<Role>();
        public ICollection<UserRoleMapper> UserRoles { get; set; } = new List<UserRoleMapper>();
    }
}
