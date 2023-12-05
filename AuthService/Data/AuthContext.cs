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

        public DbSet<Models.Type> Types { get; set; }

        public DbSet<UserGroupMapper> UserGroupMappers { get; set; }

        public DbSet<UserHoldMapper> UserHoldMappers { get; set; }

        public DbSet<UserRoleMapper> UserRoleMappers { get; set; }
        public DbSet<RightStatusMapper> RightStatusMappers { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<RightRoleMapper> RightRoleMappers { get; set; }

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

            modelBuilder
                .Entity<Status>()
                .HasMany(s => s.Rights)
                .WithMany(s => s.Statuses)
                .UsingEntity<RightStatusMapper>(
                    r => r.HasOne(p => p.Right).WithMany(p => p.RightStatus).HasForeignKey(p => p.RightId).OnDelete(DeleteBehavior.SetNull),
                    s => s.HasOne(p => p.Status).WithMany(p => p.RightStatus).HasForeignKey(p => p.StatusId).OnDelete(DeleteBehavior.SetNull)
                );

            modelBuilder
                .Entity<UserHoldMapper>()
                .HasOne(p => p.Status)
                .WithMany(p => p.UserStatus)
                .HasForeignKey(p => p.StatusId);

            modelBuilder
                .Entity<GroupHoldMapper>()
                .HasOne(p => p.StatusBoss)
                .WithMany(p => p.GroupHoldBoss)
                .HasForeignKey(p => p.StatusBossId);
            
            modelBuilder
                .Entity<GroupHoldMapper>()
                .HasOne(p => p.StatusMember)
                .WithMany(p => p.GroupHoldMember)
                .HasForeignKey(p => p.StatusMemberId);

            modelBuilder
                .Entity<Role>()
                .HasMany(s => s.Rights)
                .WithMany(s => s.Roles)
                .UsingEntity<RightRoleMapper>(
                    r => r.HasOne(p => p.Right).WithMany(p => p.RightRole).HasForeignKey(p => p.RightId).OnDelete(DeleteBehavior.SetNull),
                    s => s.HasOne(p => p.Role).WithMany(p => p.RightRole).HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.SetNull)
                );
        }
    }
}
