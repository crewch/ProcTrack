namespace AuthService.Dtos
{
    public class GetRightResponseDto
    {
        public List<string> Rights { get; set; }
        public int DestId { get; set; }
        public int UserId { get; set; }
    }
}
