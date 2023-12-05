using DB_Service.Data;
using DB_Service.Dtos.Auth.Hold;
using DB_Service.Dtos.Auth.User;
using Microsoft.AspNet.SignalR.Hosting;
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

        public async Task<HoldDto> AddGroupToHold(AddGroupToHoldRequestDto data, int id)
        {
            var content = new StringContent(
                  System.Text.Json.JsonSerializer.Serialize(data),
                  Encoding.UTF8,
                  "application/json");
            var res = await _client.PostAsync($"{_configuration["AuthService"]}/Hold/{id}/group", content);
            var resJson = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<HoldDto>(resJson);
        }

        public async Task<HoldDto> AddUserToHold(AddUserToHoldRequestDto data, int id)
        {
            var content = new StringContent(
                  System.Text.Json.JsonSerializer.Serialize(data),
                  Encoding.UTF8,
                  "application/json");
            var res = await _client.PostAsync($"{_configuration["AuthService"]}/Hold/{id}/user", content);
            var resJson = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<HoldDto>(resJson);
        }

        public async Task<List<HoldDto>> CopyHold(int HoldId, int NewHoldId)
        {
            var res = await _client.GetAsync($"{_configuration["AuthService"]}/Hold/{HoldId}/copy/{NewHoldId}");
            var resJson = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<HoldDto>>(resJson);
        }

        public async Task<HoldDto> CreateHold(CreateHoldRequestDto data)
        {
            var content = new StringContent(
                  System.Text.Json.JsonSerializer.Serialize(data),
                  Encoding.UTF8,
                  "application/json");
            var res = await _client.PostAsync($"{_configuration["AuthService"]}/Hold", content);
            var resJson = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<HoldDto>(resJson);
        }

        public async Task<HoldDto> FindHold(string Type, int DestId)
        {
            var res = await _client.GetAsync($"{_configuration["AuthService"]}/Hold/{Type}/{DestId}");
            var data = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<HoldDto>(data);
        }

        public async Task<HoldDto> GetHold(int id)
        {
            var res = await _client.GetAsync($"{_configuration["AuthService"]}/Hold/{id}");
            var data = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<HoldDto>(data);
        }

        public async Task<List<HoldDto>> GetHoldsByUserId(int UserId, string Type)
        {
            var res = await _client.GetAsync($"{_configuration["AuthService"]}/User/{UserId}/hold/{Type}");
            var data = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<HoldDto>>(data);
        }

        public async Task<UserDto> GetUser(int userId)
        {
            var res = await _client.GetAsync($"{_configuration["AuthService"]}/User/{userId}");
            var data = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDto>(data);
        }

        public async Task<List<GroupHoldDto>> Groups(int id)
        {
            var res = await _client.GetAsync($"{_configuration["AuthService"]}/Hold/{id}/group");
            var data = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GroupHoldDto>>(data);
        }

        public async Task<List<UserHoldDto>> Users(int id)
        {
            var res = await _client.GetAsync($"{_configuration["AuthService"]}/Hold/{id}/user");
            var data = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserHoldDto>>(data);
        }
    }
}
