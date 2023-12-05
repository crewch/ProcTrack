using DB_Service.Data;
using DB_Service.Exceptions;
using DB_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services.Type.CRUD
{
    public class TypeService : ITypeService
    {
        private readonly DataContext _context;

        public TypeService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Create(string title)
        {
            var newType = new Models.Type
            {
                Title = title,
            };

            await _context.AddAsync(newType);
            await _context.SaveChangesAsync();

            return newType.Id;
        }

        public async Task<int> Update(int typeId, string title)
        {
            try
            {
                var type = await Exist(typeId);

                type.Title = title;
                
                await _context.SaveChangesAsync();

                return type.Id;
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(int typeId)
        {
            try
            {
                var type = await Exist(typeId);

                _context.Types.Remove(type);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Type> Exist(int typeId)
        {
            var type = await _context.Types
                .Where(p => p.Id == typeId)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Type with id = {typeId} not found");

            return type;
        }

        public async Task<int> Find(string title)
        {
            var type = await _context.Types
                .Where(t => t.Title.ToLower() == title.ToLower())
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Type with title = {title} not found");

            return type.Id;
        }

        public async Task<string> Get(int typeId)
        {
            try
            {
                var type = await Exist(typeId);
                return type.Title;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> GetAll()
        {
            return await _context.Types
                .Select(p => p.Title)
                .ToListAsync();
        }
    }
}
