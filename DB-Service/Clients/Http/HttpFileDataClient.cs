using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DB_Service.Clients.Http
{
    public class HttpFileDataClient : IFileDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpFileDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> SendFileToS3(IFormFile file)
        {
            using (var multipartFormContent = new MultipartFormDataContent())
            {
	            var fileStreamContent = new StreamContent(file.OpenReadStream());
	            multipartFormContent.Add(fileStreamContent, name: "file", file.FileName);
	            var response = await _httpClient.PostAsync($"{_configuration["S3Service"]}/upload", multipartFormContent);
	            response.EnsureSuccessStatusCode();
	            return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<byte[]> GetFileFromS3(string fileName)
        {
            var response = await _httpClient.GetAsync($"{_configuration["S3Service"]}/download?fileName={fileName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
