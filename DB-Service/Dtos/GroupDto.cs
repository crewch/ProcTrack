using DB_Service.Dtos.Auth.User;

namespace DB_Service.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public UserDto Boss { get; set; }
    }
}
