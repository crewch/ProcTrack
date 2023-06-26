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

        public async Task<List<HoldRightsDto>> GetRightsHolds(LoginTypeDto loginType)
        {
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(loginType),
                Encoding.UTF8,
                "application/json");
            var response = await _client.PostAsync($"{_configuration["AuthService"]}/api/HoldRights", content);
            var resJson = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<List<HoldRightsDto>>(resJson);
            return res;
        }

        public async Task<HoldRightsDto> CreateHold(CreateHoldDto data)
        {
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
            var response = await _client.PostAsync($"{_configuration["AuthService"]}/api/HoldRights/CreateHold", content);
            var resJson = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<HoldRightsDto>(resJson);
            return res;
        }

        public async Task<UserWithRoles> GetUser(UserLoginDto data)
        {
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
            var response = await _client.PostAsync($"{_configuration["AuthService"]}/api/User/userWithRoles", content);
            var resJson = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<UserWithRoles>(resJson);
            return res;
        }

        public async Task<UsersGroupsDto> GetUsersGroups(GetUserByHoldDto data)
        {
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
            var response = await _client.PostAsync($"{_configuration["AuthService"]}/api/HoldRights/UsersGroups", content);
            var resJson = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<UsersGroupsDto>(resJson);
            return res;
        }
    }
}
