using DB_Service.Dtos;

namespace DB_Service.Clients.Http
{
    public interface IMailDataClient
    {
        Task<MailDto> SendMail(MailDto data);
    }
}
