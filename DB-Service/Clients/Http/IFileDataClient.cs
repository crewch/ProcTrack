using Microsoft.AspNetCore.Mvc;

namespace DB_Service.Clients.Http
{
    public interface IFileDataClient
    {
        Task<string> SendFileToS3(IFormFile File);
        Task<byte[]> GetFileFromS3(string fileName);
    }
}
