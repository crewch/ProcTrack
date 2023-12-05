using DB_Service.Data;
using DB_Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services.Dependence.CRUD
{
    public class DependenceService : IDependenceService
    {
        private readonly DataContext _context;

        public DependenceService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Create(int first, int second)
        {
            var newDependence = new Models.Dependence
            {
                First = first,
                Second = second,
            };

            await _context.Dependences.AddAsync(newDependence);
            await _context.SaveChangesAsync();

            return newDependence.Id;
        }

        public async Task<bool> Delete(int dependenceId)
        {
            try
            {
                var dependence = await Exist(dependenceId);
                
                _context.Dependences.Remove(dependence);
                await _context.SaveChangesAsync();
                
                return true;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Dependence> Exist(int dependenceId)
        {
            var dependence = await _context.Dependences
                .Where(d => d.Id == dependenceId)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Dependence with id = {dependenceId} not found");

            return dependence;
        }

        public async Task<Tuple<int?, int?>> Get(int dependenceId)
        {
            try
            {
                var dependence = await Exist(dependenceId);
                return Tuple.Create(dependence.First, dependence.Second);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
