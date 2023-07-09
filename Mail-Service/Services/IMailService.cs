using Mail_Service.Dtos;

namespace Mail_Service.Services
{
    public interface IMailService
    {
        Task<MailDto> SendAsync(MailDto mailData, CancellationToken ct);
    }
}
