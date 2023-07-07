namespace DB_Service.Dtos
{
    public class ProcessDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public string? CreatedAt { get; set; }
        public string? ApprovedAt { get; set;}
        public TimeSpan? ExpectedTime { get; set; }
        public List<HoldDto>? Hold { get; set; }
    }
}
