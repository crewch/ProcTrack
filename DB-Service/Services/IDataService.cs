using DB_Service.Models;

namespace DB_Service.Services
{
    public interface IDataService
    {
        Task<List<Message>> GetMessages();

        Task<Message> AddMessage(Message message);
    }
}
