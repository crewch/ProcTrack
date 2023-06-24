namespace DB_Service.Clients.Http
{
    public interface IFileDataClient
    {
        Task<string> SendFileToS3(IFormFile File);
    }
}
