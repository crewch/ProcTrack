using System.Threading.Tasks;
using DB_Service.Clients.Http;
using Microsoft.AspNetCore.Mvc;

namespace DB_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileDataClient _fileDataClient;
        
        public FileController(IFileDataClient fileDataClient)
        {
            _fileDataClient = fileDataClient;
        }
        [HttpPost]
        public async Task<string> UploadFile(IFormFile file)
        {
            var res = await _fileDataClient.SendFileToS3(file);
            return res;
        }
    }
}