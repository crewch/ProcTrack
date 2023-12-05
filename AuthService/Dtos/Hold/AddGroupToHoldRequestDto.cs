namespace AuthService.Dtos.Hold
{
    public class AddGroupToHoldRequestDto
    {
        public int GroupId { get; set; }
        public int StatusMemberId { get; set; }
        public int StatusBossId { get; set; }
    }
}
