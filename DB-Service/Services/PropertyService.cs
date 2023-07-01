using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;

namespace DB_Service.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly DataContext _context;
        private readonly IAuthDataClient _authClient;
        private readonly IFileDataClient _fileClient;

        public PropertyService(DataContext context, IAuthDataClient authClient, IFileDataClient fileClient)
        {
            _context = context;
            _authClient = authClient;
            _fileClient = fileClient;
        }

        public Task<List<string>> GetPriorities()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProcessDto>> GetTemplates()
        {
            throw new NotImplementedException();
        }
    }
}
