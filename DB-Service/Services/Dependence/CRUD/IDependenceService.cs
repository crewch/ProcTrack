namespace DB_Service.Services.Dependence.CRUD
{
    public interface IDependenceService
    {
        Task<int> Create(int first, int second);

        Task<Models.Dependence> Exist(int dependenceId);

        Task<Tuple<int?, int?>> Get(int dependenceId);

        Task<bool> Delete(int dependenceId);
    }
}
