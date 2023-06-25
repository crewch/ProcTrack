namespace DB_Service.Dtos
{
    public class HoldRightsDto
    {
        public int HoldId { get; set; }
        public List<string>? Rights { get; set; }
        public string User { get; set; }
        public string Group { get; set; }
    }
}
