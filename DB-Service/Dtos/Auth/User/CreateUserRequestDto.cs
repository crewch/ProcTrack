namespace DB_Service.Dtos.Auth.User
{
    public class CreateUserRequestDto
    {
        public string Email { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}
