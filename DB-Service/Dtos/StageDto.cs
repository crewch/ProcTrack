namespace DB_Service.Dtos
{
    public class StageDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Status { get; set; }
        public UserDto? User { get; set; }
        public HoldDto? Hold { get; set; }
    }
}
