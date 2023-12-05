namespace DB_Service.Services.Type.CRUD
{
    public interface ITypeService
    {
        Task<int> Create(string title);

        Task<int> Update(int typeId, string title);

        Task<bool> Delete(int typeId);

        Task<Models.Type> Exist(int typeId);

        Task<int> Find(string title);

        Task<string> Get(int priorityId);

        Task<List<string>> GetAll();
    }
}
