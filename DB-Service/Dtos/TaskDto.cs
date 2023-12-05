using DB_Service.Dtos.Auth.User;

namespace DB_Service.Dtos
{
    public class TaskDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public int StageId { get; set; }
        public int? SignId { get; set; }
        public string? StartedAt { get; set; }
        public string? ApprovedAt { get; set; }
        public TimeSpan? ExpectedTime { get; set; }
        public string? Signed { get; set; }
        public UserDto? User { get; set; }
        public List<CommentDto>? Comments { get; set; }
        public string? EndVerificationDate { get; set; }
    }
}
