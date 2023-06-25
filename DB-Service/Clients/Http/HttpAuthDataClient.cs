using DB_Service.Data;
using DB_Service.Dtos;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace DB_Service.Clients.Http
{
    public class HttpAuthDataClient : IAuthDataClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public HttpAuthDataClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<UserWithRoles>> GetUsersWithRoles()
        {
            var response = await _client.GetAsync($"{_configuration["AuthService"]}/api/Auth/debug");
            var usersJson = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserWithRoles>>(usersJson);
            return users;
        }
    }
}
