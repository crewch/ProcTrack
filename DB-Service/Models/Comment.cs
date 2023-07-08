namespace DB_Service.Models
{
    public class Comment: BaseEntity
    {
        public int? TaskId { get; set; }
        public int? UserId { get; set; }
        public Task? Task { get; set; }
        public string Text { get; set; }
        public string? FileRef { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
