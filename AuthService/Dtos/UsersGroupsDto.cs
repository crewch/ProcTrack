using AuthService.Models;

namespace AuthService.Dtos
{
    public class UsersGroupsDto
    {
        public List<UserDto> Users { get; set; }
        public List<GroupDto> Groups { get; set; }
    }
}
