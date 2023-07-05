namespace DB_Service.Models
{
    public class Stage: BaseEntity
    {
        public string Title { get; set; }
        public int? ProcessId { get; set; }
        public Process? Process { get; set; }
        public bool Addenable { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? SignedAt { get; set; }
        public int? StatusId { get; set; }
        public Status? Status { get; set; }
        public string? Signed { get; set; }
        public int? SignId { get; set; }
        public string? CustomField { get; set; }
        public ICollection<Edge> StartEdges { get; set; } = new List<Edge>();
        public ICollection<Edge> EndEdges { get; set; } = new List<Edge>();
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
        public ICollection<Dependence> FirstDependences { get; set; } = new List<Dependence>();
        public ICollection<Dependence> SecondDependences { get; set; } = new List<Dependence>();
    }
}
