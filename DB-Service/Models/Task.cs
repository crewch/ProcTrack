namespace DB_Service.Models
{
    public class Task: BaseEntity
    {
        public int? StageId { get; set; }
        public string Title { get; set; }
        public Stage? Stage { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? EndVerificationDate { get; set; } // добавить endpoint
        public DateTime? ApprovedAt { get; set; } // утверждаем
        public TimeSpan ExpectedTime { get; set; }
        public string? Signed { get; set; }
        public int? SignId { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
