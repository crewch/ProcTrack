namespace DB_Service.Models
{
    public class Status: BaseEntity
    {
        public ICollection<Stage> Stages { get; set; } = new List<Stage>();
        public string Title { get; set; }
    }
}
