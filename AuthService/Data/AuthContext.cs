using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
    public class AuthContext: DbContext
    {
        public AuthContext(DbContextOptions options) : base(options) 
        {
        }

        public DbSet<Group> Groups { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<GroupHoldMapper> GroupHoldMappers { get; set; }    

        public DbSet<Role> Roles { get;set; }

        public DbSet<Hold> Holds { get; set; }

        public DbSet<Right> Rights { get; set; }

        public DbSet<RightHoldMapper> RightHoldMappers { get; set; }

        public DbSet<Models.Type> Types { get; set; }

        public DbSet<UserGroupMapper> UserGroupMappers { get; set; }

        public DbSet<UserHoldMapper> UserHoldMappers { get; set; }

        public DbSet<UserRoleMapper> UserRoleMappers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Models.Type>()
                .HasMany(p => p.Holds)
                .WithOne(p => p.Type)
                .HasForeignKey(p => p.TypeId);

            modelBuilder
                .Entity<Hold>()
                .HasMany(p => p.Groups)
                .WithMany(p => p.Holds)
                .UsingEntity<GroupHoldMapper>(
                    g => g
                        .HasOne(p => p.Group)
                        .WithMany(p => p.GroupHolds)
                        .HasForeignKey(p => p.GroupId)
                        .OnDelete(DeleteBehavior.SetNull),
                    h => h.HasOne(p => p.Hold).WithMany(p => p.GroupHolds).HasForeignKey(p => p.HoldId).OnDelete(DeleteBehavior.SetNull)
                    );

            modelBuilder
                .Entity<Hold>()
                .HasMany(p => p.Rights)
                .WithMany(p => p.Holds)
                .UsingEntity<RightHoldMapper>(
                    r => r
                        .HasOne(p => p.Right)
                        .WithMany(p => p.RightHolds)
                        .HasForeignKey(p => p.RightId)
                        .OnDelete(DeleteBehavior.SetNull),
                    h => h
                        .HasOne(p => p.Hold)
                        .WithMany(p => p.RightHolds)
                        .HasForeignKey(p => p.HoldId)
                        .OnDelete(DeleteBehavior.SetNull)
                        
                );

            modelBuilder
                .Entity<Hold>()
                .HasMany(p => p.Users)
                .WithMany(p => p.Holds)
                .UsingEntity<UserHoldMapper>(
                    u => u.HasOne(p => p.User).WithMany(p => p.UserHolds).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.SetNull),
                    h => h.HasOne(p => p.Hold).WithMany(p => p.UserHolds).HasForeignKey(p => p.HoldId).OnDelete(DeleteBehavior.SetNull)
                );

            modelBuilder
                .Entity<User>()
                .HasMany(p => p.Groups)
                .WithMany(p => p.Users)
                .UsingEntity<UserGroupMapper>(
                    g => g.HasOne(p => p.Group).WithMany(p => p.UserGroups).HasForeignKey(p => p.GroupId).OnDelete(DeleteBehavior.SetNull),
                    u => u.HasOne(p => p.User).WithMany(p => p.UserGroups).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.SetNull)
                );
            
            modelBuilder
                .Entity<User>()
                .HasMany(p => p.Roles)
                .WithMany(p => p.Users)
                .UsingEntity<UserRoleMapper>(
                    r => r.HasOne(p => p.Role).WithMany(p => p.UserRoles).HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.SetNull),
                    u => u.HasOne(p => p.User).WithMany(p => p.UserRoles).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.SetNull)
                );
        }
    }
}
