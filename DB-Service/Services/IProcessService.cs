using DB_Service.Dtos;

namespace DB_Service.Services
{
    public interface IProcessService
    {
        Task<ProcessDto> CreateProcess(CreateProcessDto data, int UserId);

        Task<List<ProcessDto>> GetProcesesByUserId(int UserId);
        
        Task<ProcessDto> GetProcessById(int Id);
        
        Task<ProcessDto> UpdateProcess(ProcessDto data, int UserId, int Id);
        
        Task<List<StageDto>> GetStagesByProcessId(int id);
        
        Task<LinkDto> GetLinksByProcessId(int Id);
        
        Task<ProcessDto> StartProcess(int UserId, int Id);
        
        Task<ProcessDto> StopProcess(int UserId, int Id);

        Task<PassportDto> CreatePassport(CreatePassportDto data, int UserId, int Id);
        
        Task<List<PassportDto>> GetPassports(int Id);

        Task<ProcessDto?> CreateTemplate(TemplateDto data);

        Task<ProcessDto> GetProcessByStageId(int StageId);
    }
}
