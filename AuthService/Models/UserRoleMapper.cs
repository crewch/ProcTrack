﻿using Newtonsoft.Json;

namespace AuthService.Models
{
    public class UserRoleMapper : BaseEntity
    {
        public int UserId { get; set; }
        
        [JsonIgnore]
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
