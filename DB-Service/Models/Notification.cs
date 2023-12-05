namespace DB_Service.Models
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }
        public string Text { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}
