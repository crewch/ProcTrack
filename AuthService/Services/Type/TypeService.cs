using AuthService.Data;
using AuthService.Exceptions;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Services.Type
{
    public class TypeService : ITypeService
    {
        private readonly AuthContext _context;
        public TypeService(AuthContext context)
        {
            _context = context;
        }

        public async Task<Models.Type> Exist(int TypeId)
        {
            var type = await _context.Types
                .Where(h => h.Id == TypeId)
                .FirstOrDefaultAsync();

            if (type == null)
            {
                throw new NotFoundException($"Type {TypeId} not exist");
            }

            return type;
        }

        public async Task<int> Find(string Title)
        {
            var type = await _context.Types
                .Where(h => h.Title == Title)
                .FirstOrDefaultAsync();

            if (type == null)
            {
                throw new NotFoundException($"Type {Title} not exist");
            }

            return type.Id;
        }

        public async Task<string> GetTitle(int TypeId)
        {
            return await _context.Types
                .Where(t => t.Id == TypeId)
                .Select(t => t.Title)
                .FirstOrDefaultAsync();
        }
    }
}
