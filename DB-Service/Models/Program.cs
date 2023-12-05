namespace DB_Service.Models
{
    public class Program : BaseEntity
    {
        public string Title { get; set; } = String.Empty;
        public ICollection<Process> Processes { get; set; } = new List<Process>();
    }
}
