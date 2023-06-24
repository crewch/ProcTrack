using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Net.Http;
using System.Net.Http.Headers;

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
    }
}
