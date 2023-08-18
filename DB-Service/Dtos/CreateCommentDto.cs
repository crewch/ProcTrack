namespace DB_Service.Dtos
{
    public class CreateCommentDto
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public string? FileRef { get; set; }
        public string? CreatedAt { get; set; }
    }
}
