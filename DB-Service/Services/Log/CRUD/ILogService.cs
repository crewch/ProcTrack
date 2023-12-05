namespace DB_Service.Services.Log.CRUD
{
    public interface ILogService
    {
        Task<int> Create(
            string? table,
            string? field,
            string? operation,
            string? logId,
            string? oldField,
            string? newField,
            string? author
            );

        Task<Models.Log> Exist(int logId);

        Task<bool> Delete(int logId);
    }
}
