namespace DB_Service.Models
{
    public class Passport: BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int ProcessId { get; set; }
        public Process Process { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
