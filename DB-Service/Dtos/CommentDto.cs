namespace DB_Service.Dtos
{
    public class CommentDto
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public UserDto User { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
