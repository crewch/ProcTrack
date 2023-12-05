namespace AuthService.Dtos
{
    public class CreateGroupDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public UserDto Boss { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
