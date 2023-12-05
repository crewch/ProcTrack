using System.ComponentModel.DataAnnotations;

namespace DB_Service.Models
{
    public class Process: BaseEntity
    {
        public string Title { get; set; }

        public string? Description { get; set; }
        public bool IsTemplate { get; set; }
        public int? PriorityId { get; set; }
        public string? TeamcenterRef { get; set; }
        public Priority? Priority { get; set; }
        public int? TypeId { get; set; }
        public Type? Type { get; set; }
        public int? ProgramId { get; set; }
        public Program? Program { get; set; }
        public int? Head { get; set; }
        public Stage? HeadStage { get; set; }
        public int? Tail { get; set; }
        public Stage? TailStage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public TimeSpan ExpectedTime { get; set; }
        public ICollection<Stage>? Stages { get; set; } = new List<Stage>();
        public ICollection<Passport>? Passports { get; set; } = new List<Passport>();
    }
}
