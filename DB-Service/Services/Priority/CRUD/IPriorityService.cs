namespace DB_Service.Services.Priority.CRUD
{
    public interface IPriorityService
    {
        Task<int> Create(string title);

        Task<int> Update(int priorityId, string title);

        Task<bool> Delete(int priorityId);

        Task<Models.Priority> Exist(int priorityId);

        Task<int> Find(string title);

        Task<string> Get(int priorityId);

        Task<List<string>> GetAll();
    }
}
