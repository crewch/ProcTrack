namespace DB_Service.Services.Status.CRUD
{
    public interface IStatusService
    {
        Task<int> Create(string title);

        Task<int> Update(int statusId, string title);

        Task<bool> Delete(int statusId);

        Task<Models.Status> Exist(int statusId);

        Task<int> Find(string title);

        Task<string> Get(int statusId);

        Task<List<string>> GetAll();

        Task<int> NotStarted();

        Task<int> InRework();

        Task<int> SentForVerification();

        Task<int> AcceptedForVerification();

        Task<int> AssignedBlocked();

        Task<int> Assigned();
            
        Task<int> Stopped();
    }
}
