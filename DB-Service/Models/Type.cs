namespace DB_Service.Models
{
    public class Type: BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Process> Processes { get; set; } = new List<Process>();
    }
}
