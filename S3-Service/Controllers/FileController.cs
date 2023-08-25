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
using Minio.Exceptions;
using System.Text.RegularExpressions;
using Minio.DataModel;
using System.Security.Cryptography;
using Google.Protobuf.WellKnownTypes;

namespace S3_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
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
            var bea = new BucketExistsArgs()
                .WithBucket(BucketName);

            bool found = await _minioClient.BucketExistsAsync(bea);

            _logger.LogInformation($"{found}");

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
                stream.Seek(0, SeekOrigin.Begin);

                _logger.LogInformation($"File copied to stream {stream.Name} {stream.Length}.");

                //if (!await _minioClient.BucketExistsAsync(BucketName))
                //    await _minioClient.MakeBucketAsync(BucketName);

                //_logger.LogInformation("Bucket exists/created, uploading file...");

                //stream.Seek(0, SeekOrigin.Begin);

                //await _minioClient.PutObjectAsync(BucketName, fileName, stream, stream.Length, file.ContentType);
                PutObjectArgs putObjectArgs = new PutObjectArgs()
                                      .WithBucket(BucketName)
                                      .WithObject(fileName)
                                      .WithObjectSize(stream.Length)
                                      .WithStreamData(stream)
                                      .WithContentType(file.ContentType);
                
                await _minioClient.PutObjectAsync(putObjectArgs);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }

            return Ok(new {FileName = fileName});
        }

        [HttpPost("/download")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return BadRequest($"'{nameof(fileName)} cannot be null or empty!'");

            var filePath = Path.GetTempFileName();
            _logger.LogInformation($"Temp file name: '{filePath}'.");
            
            var stream = System.IO.File.Create(filePath);
            stream.Seek(0, SeekOrigin.Begin);
            //{
            //    await _minioClient.GetObjectAsync(BucketName, fileName, async callbackStream => await callbackStream.CopyToAsync(stream));
            
            StatObjectArgs statObjectArgs = new StatObjectArgs()
                                       .WithBucket(BucketName)
                                       .WithObject(fileName);

            await _minioClient.StatObjectAsync(statObjectArgs);

            GetObjectArgs getObjectArgs = new GetObjectArgs()
                                     .WithBucket(BucketName)
                                     .WithObject(fileName)
                                     .WithCallbackStream((s) =>
                                          {
                                              s.CopyTo(stream);
                                          });

            await _minioClient.GetObjectAsync(getObjectArgs);

            stream.Seek(0, SeekOrigin.Begin);

            if (!new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType))
                contentType = "application/octet-stream";

            
            return File(stream, contentType, fileName);
        }

        //[HttpPost("/download")]
        //public async Task<IActionResult> DownloadFile(string fileName)
        //{
        //    if (string.IsNullOrEmpty(fileName))
        //        return BadRequest($"'{nameof(fileName)} cannot be null or empty!'");

        //    //if (!Guid.TryParse(fileName.Split('.').FirstOrDefault(), out _))
        //    //    return BadRequest($"Invalid '{fileName}'!");

        //    var filePath = Path.GetTempFileName();
        //    _logger.LogInformation($"Temp file name: '{filePath}'.");



        //    var stat = await _minioClient.StatObjectAsync(BucketName, fileName);
            
        //    _logger.LogInformation($"{stat.ObjectName} {stat.Size} {stat.ContentType}");

        //    return null;

        //    //StatObjectArgs statObjectArgs = new StatObjectArgs()
        //    //                            .WithBucket("mybucket")
        //    //                            .WithObject("myobject");

        //    //await _minioClient.StatObjectAsync(statObjectArgs);

        //    //using (var stream = System.IO.File.Create(filePath))
        //    //{
        //    //    await _minioClient.GetObjectAsync(BucketName, fileName, async callbackStream => await callbackStream.CopyToAsync(stream));
            
        //    //    //stream.Seek(0, SeekOrigin.Begin);

        //    //    if (!new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType))
        //    //        contentType = "application/octet-stream";

        //    //    return File(stream, contentType, fileName);
        //    //}
        //}
    }
}