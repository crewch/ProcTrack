namespace DB_Service.Services.Program.CRUD
{
    public interface IProgramService
    {
        Task<int> Create(string title);

        Task<int> Update(int programId, string title);

        Task<bool> Delete(int programId);

        Task<List<string>> GetAll();

        Task<Models.Program> Exist(int programId);

        Task<string> Get(int programId);

        Task<int> Find(string title);
    }
}
