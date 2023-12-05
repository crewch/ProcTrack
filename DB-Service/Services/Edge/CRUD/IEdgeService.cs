namespace DB_Service.Services.Edge.CRUD
{
    public interface IEdgeService
    {
        Task<int> Create(int start, int end);

        Task<bool> Delete(int edgeId);

        Task<Models.Edge> Exist(int edgeId);
        
        Task<Tuple<int?, int?>> Get(int edgeId);
    }
}
