namespace DB_Service.Dtos.Auth.Group
{
    public class CreateGroupRequestDto
    {
        public string Title { get; set; }
        public int BossUserId { get; set; }
    }
}
