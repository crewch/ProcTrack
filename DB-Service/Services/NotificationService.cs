
using DB_Service.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace DB_Service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async void SendNotification(int Id, int UserId, string type)
        {
            var userConnections = NotificationHub.GetConnections(UserId);

            Console.WriteLine($"send {type} notification to user {UserId}");

            foreach (var connection in userConnections)
            {
                await _hubContext.Clients.Client(connection).SendAsync($"{type}Notification", Id);
            }
        }
    }
}
