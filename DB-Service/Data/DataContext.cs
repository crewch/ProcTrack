using DB_Service.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DB_Service.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) 
        { 
        }
        
        public DbSet<Comment> Comments { get; set; }
        
        public DbSet<Dependence> Dependences { get; set; }
        
        public DbSet<Edge> Edges { get; set; }
        
        public DbSet<Passport> Passports { get; set; }

        public DbSet<Process> Processes { get; set; }

        public DbSet<Priority> Priorities { get; set; }

        public DbSet<Stage> Stages { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Models.Task> Tasks { get; set; }

        public DbSet<Models.Type> Types { get; set; }

        public DbSet<Models.Program> Programs { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Status>()
                .HasMany(p => p.Stages)
                .WithOne(p =>  p.Status)
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder 
                .Entity<Models.Task>()
                .HasMany(p => p.Comments)
                .WithOne(p => p.Task)
                .HasForeignKey(p => p.TaskId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Stage>()
                .HasMany(p => p.StartEdges)
                .WithOne(p => p.StartStage)
                .HasForeignKey(p => p.Start)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Stage>()
                .HasMany(p => p.EndEdges)
                .WithOne(p => p.EndStage)
                .HasForeignKey(p => p.End)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Stage>()
                .HasMany(p => p.Tasks)
                .WithOne(p => p.Stage)
                .HasForeignKey(p => p.StageId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Stage>()
                .HasMany(p => p.FirstDependences)
                .WithOne(p => p.FirstStage)
                .HasForeignKey(p => p.First)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Stage>()
                .HasMany(p => p.SecondDependences)
                .WithOne(p => p.SecondStage)
                .HasForeignKey(p => p.Second)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Process>()
                .HasMany(p => p.Stages)
                .WithOne(p => p.Process)
                .HasForeignKey(p => p.ProcessId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Process>()
                .HasMany(p => p.Passports)
                .WithOne(p => p.Process)
                .HasForeignKey(p => p.ProcessId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Priority>()
                .HasMany(p => p.Processes)
                .WithOne(p => p.Priority)
                .HasForeignKey(p => p.PriorityId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Models.Type>()
                .HasMany(p => p.Processes)
                .WithOne(p => p.Type)
                .HasForeignKey(p => p.TypeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Models.Program>()
                .HasMany(p => p.Processes)
                .WithOne(p => p.Program)
                .HasForeignKey(p => p.ProgramId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Process>()
                .HasOne(p => p.HeadStage)
                .WithOne()
                .HasForeignKey<Process>(p => p.Head)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Process>()
                .HasOne(p => p.TailStage)
                .WithOne()
                .HasForeignKey<Process>(p => p.Tail)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
