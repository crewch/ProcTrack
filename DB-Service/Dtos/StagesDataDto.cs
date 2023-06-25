using DB_Service.Models;

namespace DB_Service.Dtos
{
    public class StagesDataDto
    {
        public int Id { get; set; }
        public List<string>? Rights { get; set; }
        public string User { get; set; }
        public string Group { get; set; }
        public string Title { get; set; }
        //public int? ProcessId { get; set; }
        //public Process? Process { get; set; }
        public bool Addenable { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? SignedAt { get; set; }
        public string Status { get; set; }
        public string? Signed { get; set; }
        public string? CustomField { get; set; }
    }
}
