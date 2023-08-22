namespace DB_Service.Services
{
    public interface INotificationService
    {
        void SendNotification(int ProcessId, int StageId, int UserId, string type);
    }
}
