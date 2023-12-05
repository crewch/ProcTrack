namespace DB_Service.Services.Notification.CRUD
{
    public interface INotificationService
    {
        Task<int> Create(int userId, string text);

        Task<Models.Notification> Exist(int notificationId);

        Task<bool> Delete(int notificationId);
    }
}
