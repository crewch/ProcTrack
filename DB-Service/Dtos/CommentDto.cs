using DB_Service.Dtos.Auth.User;

namespace DB_Service.Dtos
{
    public class CommentDto
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public string? FileRef { get; set; }
        public UserDto User { get; set; }
        public string? CreatedAt { get; set; }
    }
}
