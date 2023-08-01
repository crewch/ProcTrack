using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;
using DB_Service.Tools;
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

        public async Task<List<string>> GetPriorities()
        {
            return await _context.Priorities
                .Select(p => p.Title)
                .ToListAsync();
        }

        public async Task<List<string>> GetStageStatuses()
        {
            return await _context.Statuses
                .Select(s => s.Title)
                .ToListAsync();
        }

        public async Task<List<string>> GetProcessStatuses()
        {
            var res = new List<string>()
            {
                "остановлен",
                "отменен",
                "завершен",
                "в процессе"
            };

            return res;
        }

        public async Task<List<string>> GetTypes()
        {
            return await _context.Types
                .Select(p => p.Title)
                .ToListAsync();
        }

        public async Task<List<ProcessDto>> GetTemplates()
        {
            var templates = await _context.Processes
                .Include(t => t.Type)
                .Include(t => t.Priority)
                .Where(p => p.IsTemplate)
                .ToListAsync();

            var templateDtos = new List<ProcessDto>();

            foreach (var iTemplate in templates)
            {
                templateDtos.Add(new ProcessDto
                {
                    Id = iTemplate.Id,
                    Title = iTemplate.Title,
                    Type = iTemplate.Type == null ? null : iTemplate.Type.Title,
                    Priority = iTemplate.Priority == null ? null : iTemplate.Priority.Title,
                    CreatedAt = iTemplate.CreatedAt == null ? null : DateParser.Parse((DateTime)iTemplate.CreatedAt),
                    ApprovedAt = iTemplate.ApprovedAt == null ? null : DateParser.Parse((DateTime)iTemplate.ApprovedAt),
                    ExpectedTime = iTemplate.ExpectedTime,
                });
            }
            return templateDtos;
        }

    }
}
