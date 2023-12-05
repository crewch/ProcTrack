namespace DB_Service.Dtos.Auth.Hold
{
    public class AddUserToHoldRequestDto
    {
        public int UserId { get; set; }
        public int StatusId { get; set; }
    }
}
