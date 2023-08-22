namespace DB_Service.Services
{
    public interface INotificationService
    {
        void SendNotification(int Id, int UserId, string type);
    }
}
