namespace DB_Service.Models
{
    public class Task: BaseEntity
    {
        public int StageId { get; set; }
        public Stage Stage { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndVerificationDate { get; set; }
        public DateTime ApprovedAt { get; set; }
        public TimeSpan ExpectedTime { get; set; }
        public string Signed { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
