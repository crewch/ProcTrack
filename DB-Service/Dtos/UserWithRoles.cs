namespace DB_Service.Dtos
{
    public class UserWithRoles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
