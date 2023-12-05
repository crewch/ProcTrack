using Npgsql.PostgresTypes;

namespace AuthService.Services.Role
{
    public interface IRoleService
    {
        Task<Models.Role> Exist(int RoleId);
        Task<int> Find(string Title);
        Task<List<string>> Rights(int RoleId);
    }
}
