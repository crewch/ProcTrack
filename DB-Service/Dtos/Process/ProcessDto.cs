using DB_Service.Dtos.Auth.Hold;

namespace DB_Service.Dtos.Process
{
    public class ProcessDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public int? PriorityValue { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public string? Program { get; set; }
        public string? CreatedAt { get; set; }
        public string? CompletedAt { get; set; }
        public DateTime? CompletedAtUnparsed { get; set; }
        public string? ApprovedAt { get; set; }
        public TimeSpan? ExpectedTime { get; set; }
        public HoldDto? Hold { get; set; }
    }
}
