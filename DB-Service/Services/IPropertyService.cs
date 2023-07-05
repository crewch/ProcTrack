using DB_Service.Dtos;
using DB_Service.Models;

namespace DB_Service.Services
{
    public interface IPropertyService
    {
        Task<List<ProcessDto>> GetTemplates();

        Task<List<string>> GetPriorities();
    }
}
