namespace AuthService.Dtos
{
    public class HoldDto
    {
        public int Id { get; set; }
        public int DestId { get; set; }
        public string Type { get; set; }
        public List<string>? Rights { get; set; }
        public List<UserDto>? Users { get; set; }
        public List<GroupDto>? Groups { get; set; }
    }
}

// +