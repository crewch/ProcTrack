namespace DB_Service.Dtos
{
    public class ProcessDto
    {
        public int? Id { get; set; }
        public string? Priority { get; set; }
        public string? Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set;}
        public TimeSpan? ExpectedTime { get; set; }
        public HoldDto? Hold { get; set; }
    }
}
