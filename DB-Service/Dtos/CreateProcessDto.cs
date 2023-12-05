using DB_Service.Dtos.Process;

namespace DB_Service.Dtos
{
    public class CreateProcessDto
    {
        public int TemplateId { get; set; }
        public int? GroupId { get; set; }
        public ProcessDto Process { get; set; }
    }
}
