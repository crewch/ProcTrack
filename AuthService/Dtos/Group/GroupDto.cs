using AuthService.Dtos.User;

namespace AuthService.Dtos.Group
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BossUserId { get; set; }
    }
}
