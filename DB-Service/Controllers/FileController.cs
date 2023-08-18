using System.Threading.Tasks;
using DB_Service.Clients.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DB_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    // [EnableCors]
    [Authorize]

    public class FileController : ControllerBase
    {
        private readonly IFileDataClient _fileDataClient;
        
        public FileController(IFileDataClient fileDataClient)
        {
            _fileDataClient = fileDataClient;
        }
        [Route("upload")]
        [HttpPost]
        public async Task<string> UploadFile(IFormFile file)
        {
            var res = await _fileDataClient.SendFileToS3(file);
            return res;
        }
        [Route("download")]
        [HttpPost]
        public async Task<IActionResult> Download(string fileName)
        {
            var fileBytes = await _fileDataClient.GetFileFromS3(fileName);

            return File(fileBytes, "application/octet-stream", fileName);
        }
    }
}