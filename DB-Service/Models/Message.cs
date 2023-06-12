namespace DB_Service.Models
{
    public class Message: BaseEntity
    {
        public string UserName { get; set; }

        public string MessageContent { get; set; }
    }
}
