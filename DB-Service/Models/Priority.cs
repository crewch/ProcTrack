namespace DB_Service.Models
{
    public class Priority: BaseEntity
    {
        public string Title { get; set; }
        public int Value { get; set; }
        public ICollection<Process> Processes { get; set; } = new List<Process>();
    }
}
