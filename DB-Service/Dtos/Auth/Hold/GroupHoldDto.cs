namespace DB_Service.Dtos.Auth.Hold
{
    public class GroupHoldDto
    {
        public int GroupId { get; set; }
        public int StatusMemberId { get; set; }
        public int StatusBossId { get; set; }
    }
}
