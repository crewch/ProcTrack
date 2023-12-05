using DB_Service.Dtos;
using Newtonsoft.Json;
using System.Text;

namespace DB_Service.Clients.Http
{
    public class HttpMailDataClient : IMailDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpMailDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<MailDto> SendMail(MailDto data)
        {
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
            Console.WriteLine($"{_configuration["MailService"]}/Mail/sendmail");
            var resFromServer = await _httpClient.PostAsync($"{_configuration["MailService"]}/Mail/sendmail", content);
            var resJson = await resFromServer.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<MailDto>(resJson);
            return res;
        }
    }
}
