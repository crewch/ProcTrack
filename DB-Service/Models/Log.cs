namespace DB_Service.Models
{
    public class Log: BaseEntity
    {
        public string Table { get; set; }
        public string Field { get; set; }
        public string Operation { get; set; }
        public string LogId { get; set; }
        public string Old { get; set; }
        public string New { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
