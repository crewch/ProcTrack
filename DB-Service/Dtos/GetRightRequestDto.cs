namespace AuthService.Dtos
{
    public class GetRightRequest
    {
        public int DestId { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
    }
}
