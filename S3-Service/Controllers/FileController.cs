using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Minio;
using System.Diagnostics;
using Minio.Exceptions;
using System.Text.RegularExpressions;

namespace S3_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    //[EnableCors("cors")]
    public class FilesController : ControllerBase
    {
        private const string BucketName = "test";

        private readonly ILogger<FilesController> _logger;
        private readonly MinioClient _minioClient;

        public FilesController(
            ILogger<FilesController> logger,
            MinioClient minioClient)
        {
            _logger = logger;
            _minioClient = minioClient;
        }

        [HttpPost("/upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file is null)
                return BadRequest("Must upload a valid file!");

            if (file.Length > 15728640) {
                return BadRequest("Размер файла не должен превышать 15 МБ");
            }

            //Stopwatch stopwatch = Stopwatch.StartNew();
            //var fileId = Guid.NewGuid().ToString();
            var name = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);
            var str1 = DateTime.Now.ToString();
            var str2 = Regex.Replace(str1, "[ :/.,]", "_");
            var fileName = $"{name}_{str2}{extension}";

            var filePath = Path.GetTempFileName();

            _logger.LogInformation($"Temp file name: '{filePath}'.");

            try
            {
                await using var stream = System.IO.File.Create(filePath);
                await file.CopyToAsync(stream);

                _logger.LogInformation("File copied to stream.");

                if (!await _minioClient.BucketExistsAsync(BucketName))
                    await _minioClient.MakeBucketAsync(BucketName);

                _logger.LogInformation("Bucket exists/created, uploading file...");

                stream.Seek(0, SeekOrigin.Begin);

                await _minioClient.PutObjectAsync(BucketName, fileName, stream, stream.Length, file.ContentType);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }

            return Ok(new {FileName = fileName});
        }

        [HttpGet("/download")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return BadRequest($"'{nameof(fileName)} cannot be null or empty!'");

            //if (!Guid.TryParse(fileName.Split('.').FirstOrDefault(), out _))
            //    return BadRequest($"Invalid '{fileName}'!");

            var filePath = Path.GetTempFileName();
            _logger.LogInformation($"Temp file name: '{filePath}'.");

            var stream = System.IO.File.Create(filePath);
            
            try
            {
                await _minioClient.GetObjectAsync(
                    BucketName,
                    fileName,
                    async callbackStream => await callbackStream.CopyToAsync(stream));
            }
            catch (ObjectNotFoundException exception)
            {
                _logger.LogWarning(exception.Message);
                
                stream.Close();
                
                return NotFound(exception.Message);
            }
            
            stream.Seek(0, SeekOrigin.Begin);

            if (!new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType))
                contentType = "application/octet-stream";

            return File(stream, contentType, fileName);
        }
    }
}