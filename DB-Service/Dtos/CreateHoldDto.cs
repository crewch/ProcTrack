namespace DB_Service.Dtos
{
    public class CreateHoldDto
    {
        public int DestId { get; set; }
        public string Type { get; set; }
        public string HolderType { get; set; }
        public int HolderTypeId { get; set; }
    }
}
