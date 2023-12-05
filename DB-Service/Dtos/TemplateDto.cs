using DB_Service.Dtos.Process;
using DB_Service.Dtos.Stage;

namespace DB_Service.Dtos
{
    public class TemplateDto
    {
        public ProcessDto? Process { get; set; }
        public int? StartStage { get; set; }
        public int? EndStage { get; set; }
        public List<StageDto>? Stages { get; set; }
        public List<TaskDto>? Tasks { get; set; }
        public LinkDto? Links { get; set; }
    }
}
