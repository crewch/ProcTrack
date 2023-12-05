using DB_Service.Dtos.Process;

namespace DB_Service.Services.Process.CRUD
{
    public interface IProcessService
    {
        Task<int> Create(
            string title, 
            string? description, 
            bool isTemplate, 
            int? priorityId,
            int? programId,
            int? typeId, 
            int? head, 
            int? tail, 
            TimeSpan? expectedTime
            );

        Task<ProcessDto> Get(int processId);

        Task<Models.Process> Exist(int processId);

        Task<string> GetStatus(int processId);

        Task<List<string>> Statuses();

        Task<int> Update(
            int processId,
            string? title,
            string? description,
            int? priorityId,
            int? programId,
            int? typeId,
            TimeSpan? expectedTime
            );

        Task<bool> Delete(int processId);

        Task<List<int>> Stages(int processId);

        Task<List<int>> VisibleStages(int processId);

        Task<List<int>> CopyStages(int oldProcessId, int newProcessId);

        Task<List<Tuple<int, int>>> Edges(int processId);

        Task<List<Tuple<int, int>>> Dependences(int processId);

        Task<List<int>> Passports(int processId);
    }
}
