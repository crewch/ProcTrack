using DB_Service.Dtos;
using DB_Service.Models;
using Microsoft.AspNetCore.Identity;

namespace DB_Service.Services
{
    public interface IDataService
    {
        //Task<List<Message>> GetMessages();

        //Task<Message> AddMessage(Message message);

        Task<List<StagesDataDto>> GetStageData(LoginTypeDto loginType);

        Task<List<ProcessesDataDto>> GetProcessData(LoginTypeDto loginType);
    }
}
