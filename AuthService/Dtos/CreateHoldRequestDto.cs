namespace AuthService.Dtos
{
    public class CreateHoldRequestDto
    {
        public int DestId { get; set; }
        public string DestType { get; set; }
        public string HolderType { get; set; }
        public int HolderId { get; set; }
    }
}
