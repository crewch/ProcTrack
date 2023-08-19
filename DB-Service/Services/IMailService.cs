using DB_Service.Dtos;
using DB_Service.Models;

namespace DB_Service.Services
{
    public interface IMailService
    {
        void SendProcessMailToChecker(Process process, UserDto user, GroupDto group, Stage stage);
        void SendProcessMailToReleaser(Process process, Stage stage, UserDto user);
    }
}
