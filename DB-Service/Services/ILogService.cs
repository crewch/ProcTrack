using DB_Service.Models;

namespace DB_Service.Services
{
    public interface ILogService
    {
        Task<Log> CreateLog(Log data);
    }
}
