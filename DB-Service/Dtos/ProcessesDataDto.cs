using DB_Service.Models;

namespace DB_Service.Dtos
{
    public class ProcessesDataDto
    {
        public int Id { get; set; }
        public List<string>? Rights { get; set; }
        public string User { get; set; }
        public string Group { get; set; }
        public string Title { get; set; }
        public bool IsTemplate { get; set; }
        public string? Priority { get; set; }
        public string? Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
    }
}
