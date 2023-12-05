using DB_Service.Data;
using DB_Service.Dtos;
using DB_Service.Exceptions;
using DB_Service.Tools;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace DB_Service.Services.Passport.CRUD
{
    public class PassportService : IPassportService
    {
        private readonly DataContext _context;

        public PassportService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Create(int processId, string title, string message)
        {
            var newPassport = new Models.Passport
            {
                ProcessId = processId,
                Title = title,
                Message = message,
                CreatedAt = DateTime.Now.AddHours(3),
            };

            await _context.Passports.AddAsync(newPassport);
            await _context.SaveChangesAsync();

            return newPassport.Id;
        }

        public async Task<int> Update(int passportId, string? title, string? message)
        {
            try
            {
                var passport = await Exist(passportId);

                passport.Title = title ?? passport.Title;
                passport.Message = message ?? passport.Message;

                await _context.SaveChangesAsync();

                return passport.Id;
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            } 
        }

        public async Task<int> Delete(int passportId)
        {
            try
            {
                var passport = await Exist(passportId);

                int processId = passport.ProcessId;

                _context.Passports.Remove(passport);
                await _context.SaveChangesAsync();

                return processId;
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }
        }

        public async Task<Models.Passport> Exist(int passportId)
        {
            var passport = await _context.Passports
                .Include(c => c.Process)
                .Where(c => c.Id == passportId)
                .FirstOrDefaultAsync() ?? 
                throw new NotFoundException($"Passport with id = {passportId} not found");
            
            return passport;
        }

        public async Task<PassportDto> Get(int passportId)
        {
            try
            {
                var passport = await Exist(passportId);

                return new PassportDto
                {
                    Id = passport.Id,
                    ProcessId = passport.ProcessId,
                    Title = passport.Title,
                    Message = passport.Message,
                    CreatedAt = DateParser.Parse(passport.CreatedAt)
                };
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }
        }
    }
}
