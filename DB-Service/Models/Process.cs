using System.ComponentModel.DataAnnotations;

namespace DB_Service.Models
{
    public class Process: BaseEntity
    {
        public string Title { get; set; }
        public bool IdTemplate { get; set; }
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public int Head { get; set; }
        public Stage HeadStage { get; set; }
        public int Tail { get; set; }
        public Stage TailStage { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Stage> Stages { get; set; } = new List<Stage>();
        public ICollection<Passport> Passports { get; set; } = new List<Passport>();
    }
}
