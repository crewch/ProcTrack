namespace DB_Service.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public List<string>? Roles { get; set; }
    }
}
