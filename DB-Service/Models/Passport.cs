namespace DB_Service.Models
{
    public class Passport: BaseEntity
    {
        public string Title { get; set; } = String.Empty;
        public string Message { get; set; } = String.Empty;
        public int ProcessId { get; set; }
        public Process Process { get; set; } = new Process();
        public DateTime CreatedAt { get; set; }
    }
}
