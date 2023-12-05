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

        public async Task<CreateHoldResponceDto> CreateHold(CreateHoldRequestDto data)
        {
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
            var resFromServer = await _client.PostAsync($"{_configuration["AuthService"]}/Hold/create", content);
            var resJson = await resFromServer.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<CreateHoldResponceDto>(resJson);
            return res;
        }

        public async Task<HoldDto> GetHoldById(int id)
        {
            var response = await _client.GetAsync($"{_configuration["AuthService"]}/Hold/{id}");
            var data = await response.Content.ReadAsStringAsync();
            var hold = JsonConvert.DeserializeObject<HoldDto>(data);
            return hold;
        }

        public async Task<List<HoldDto>> GetHolds(UserHoldTypeDto data)
        {
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
            var resFromServer = await _client.PostAsync($"{_configuration["AuthService"]}/Hold/get", content);
            var resJson = await resFromServer.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<List<HoldDto>>(resJson);
            return res;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var response = await _client.GetAsync($"{_configuration["AuthService"]}/User/{id}");
            var data = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDto>(data);
            return user;
        }

        public async Task<List<HoldDto>> FindHold(int destId, string type)
        {
            var response = await _client.GetAsync($"{_configuration["AuthService"]}/Hold/find?destId={destId}&type={type}");
            var data = await response.Content.ReadAsStringAsync();
            var holds = JsonConvert.DeserializeObject<List<HoldDto>>(data);
            return holds;
        }

        public async Task<List<UserDto>> GetUsersByGroupId(int GroupId)
        {
            var response = await _client.GetAsync($"{_configuration["AuthService"]}/User/Groups/{GroupId}/Users");
            var data = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserDto>>(data);
            return users;
        }
    }
}
