using DB_Service.Data;
using DB_Service.Exceptions;
using DB_Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DB_Service.Services.Edge.CRUD
{
    public class EdgeService : IEdgeService
    {
        private readonly DataContext _context;

        public EdgeService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Create(int start, int end)
        {
            var newEdge = new Models.Edge
            {
                Start = start,
                End = end,
            };

            await _context.Edges.AddAsync(newEdge);
            await _context.SaveChangesAsync();

            return newEdge.Id;
        }

        public async Task<bool> Delete(int edgeId)
        {
            try
            {
                var edge = await Exist(edgeId);
                
                _context.Edges.Remove(edge);
                await _context.SaveChangesAsync();
                
                return true;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Edge> Exist(int edgeId)
        {
            var edge = await _context.Edges
                .Where(e => e.Id == edgeId)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Edge with id = {edgeId} not found");

            return edge;
        }

        public async Task<Tuple<int?, int?>> Get(int edgeId)
        {
            try
            {
                var edge = await Exist(edgeId);
                return Tuple.Create(edge.Start, edge.End);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
