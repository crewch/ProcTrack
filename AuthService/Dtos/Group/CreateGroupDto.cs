using AuthService.Dtos.User;

namespace AuthService.Dtos.Group
{
    public class CreateGroupRequestDto
    {
        public string Title { get; set; }
        public int BossUserId { get; set; }
    }
}
