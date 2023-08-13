namespace DB_Service.Dtos
{
    public class FilterProcessDto
    {
        public string? Text { get; set; }
        public List<string>? Types { get; set; }
        public List<string>? Statuses { get; set; }
        public List<string>? Priorities { get; set; }
        public bool ShowCompleted { get; set; } = false;
    }
}
