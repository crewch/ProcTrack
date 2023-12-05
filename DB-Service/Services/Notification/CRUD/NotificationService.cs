using DB_Service.Data;
using DB_Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services.Notification.CRUD
{
    public class NotificationService : INotificationService
    {
        private readonly DataContext _context;

        public NotificationService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Create(int userId, string text)
        {
            var newNotification = new Models.Notification
            {
                UserId = userId,
                Text = text
            };

            await _context.Notifications.AddAsync(newNotification);
            await _context.SaveChangesAsync();

            return newNotification.Id;
        }

        public async Task<bool> Delete(int notificationId)
        {
            try
            {
                var notification = await Exist(notificationId);

                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Notification> Exist(int notificationId)
        {
            var notification = await _context.Notifications
                .Where(n => n.Id == notificationId)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Notification with Id = {notificationId} not found");

            return notification;
        }
    }
}
