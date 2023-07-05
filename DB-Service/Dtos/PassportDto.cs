namespace DB_Service.Dtos
{
    public class PassportDto
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
    }
}