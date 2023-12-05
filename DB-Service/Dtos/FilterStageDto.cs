namespace DB_Service.Dtos
{
    public class FilterStageDto
    {
        public string? Text { get; set; }
        public List<string>? Statuses { get; set; }
        public List<string>? Types { get; set; }
        public List<string>? Priorities { get; set;}
        public bool ShowApproved { get; set; } = false;
    }
}
