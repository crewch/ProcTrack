using AuthService.Models;

namespace AuthService.Services.Type
{
    public interface ITypeService
    {
        Task<Models.Type> Exist(int TypeId);
        Task<int> Find(string Title);
        Task<string> GetTitle(int TypeId);
    }
}
