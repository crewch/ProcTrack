namespace DB_Service.Dtos
{
    public class StageDto
    {
        public int Id { get; set; }
        public int? ProcessId { get; set; }
        public string? ProcessName { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public string? CreatedAt { get; set; }
        public DateTime? CreatedAtUnparsed { get; set; }
        public string? SignedAt { get; set; }
        public string? ApprovedAt { get; set; }
        public string Status { get; set; }
        public int? StatusValue { get; set; }
        public UserDto? User { get; set; }
        public List<HoldDto>? Holds { get; set; }
        public List<int>? CanCreate { get; set; }
        public bool? Mark { get; set; }
        public bool? Pass { get; set; }
        public FilterStageDto? Filter { get; set; }
    }
}
