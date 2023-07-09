using Mail_Service.Dtos;

namespace Mail_Service.Services
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailDto mailData, CancellationToken ct);
    }
}
