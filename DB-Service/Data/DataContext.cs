using DB_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<Message> Messages { get; set; }
    }
}
