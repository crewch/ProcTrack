using AuthService.Models;

namespace AuthService.Services.Status
{
    public interface IStatusService
    {
        // проверка существования
        Task<Models.Status> Exist(int StatusId);
        // создать новый статус
        // получить права статуса
        Task<List<string>> Rights(int StatusId);
    }
}
