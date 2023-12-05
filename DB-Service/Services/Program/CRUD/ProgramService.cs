using DB_Service.Data;
using DB_Service.Exceptions;
using DB_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services.Program.CRUD
{
    public class ProgramService : IProgramService
    {
        private readonly DataContext _context;

        public ProgramService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Create(string title)
        {
            var newProgram = new Models.Program
            {
                Title = title,
            };

            await _context.Programs.AddAsync(newProgram);
            await _context.SaveChangesAsync();

            return newProgram.Id;
        }

        public async Task<int> Update(int programId, string title)
        {
            try
            {
                var program = await Exist(programId);

                program.Title = title;
                
                await _context.SaveChangesAsync();

                return program.Id;
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }  
        }

        public async Task<bool> Delete(int programId)
        {
            try
            {
                var program = await Exist(programId);

                _context.Programs.Remove(program);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Program> Exist(int programId)
        {
            var program = await _context.Programs
                .Where(p => p.Id == programId)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Program with id = {programId} not found");

            return program;
        }

        public async Task<int> Find(string title)
        {
            var program = await _context.Programs
                .Where(p => p.Title.ToLower() == title)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Program with title = {title} not found");
            
            return program.Id;
        }

        public async Task<string> Get(int programId)
        {
            try
            {
                var program = await Exist(programId);
                return program.Title;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> GetAll()
        {
            return await _context.Programs
                .Select(p => p.Title)
                .ToListAsync();
        }
    }
}
