namespace DB_Service.Dtos
{
    public class FromTemplateCreateDto
    {
        public string Email { get; set; }
        public string Title { get; set; }
        public int PriorityId { get; set; }
        public int TemplateId { get; set; }
        public int GroupId { get; set; }
    }
}
