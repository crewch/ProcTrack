using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;
using Microsoft.EntityFrameworkCore;

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
            var priorities = _context.Priorities
                .Select(p => p.Title)
                .ToList();
            return Task.FromResult(priorities);
        }

        public Task<List<ProcessDto>> GetTemplates()
        {
            var templates = _context.Processes
                .Include(t => t.Type)
                .Include(t => t.Priority)
                .Where(p => p.IsTemplate)
                .ToList();

            var templateDtos = new List<ProcessDto>();

            foreach (var iTemplate in templates)
            {
                templateDtos.Add(new ProcessDto
                {
                    Id = iTemplate.Id,
                    Type = iTemplate.Type.Title,
                    Priority = iTemplate.Priority.Title, 
                    CreatedAt = iTemplate.CreatedAt,
                    ApprovedAt = iTemplate.ApprovedAt,
                    ExpectedTime = iTemplate.ExpectedTime,
                });
            }
            return Task.FromResult(templateDtos);
        }
    }
}
