namespace DB_Service.Dtos
{
    public class StageDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Status { get; set; }
        public int? StatusValue { get; set; }
        public UserDto? User { get; set; }
        public List<HoldDto>? Holds { get; set; }
        public List<int>? CanCreate { get; set; }
        public bool? Mark { get; set; }
        public bool? Pass { get; set; }
    }
}
