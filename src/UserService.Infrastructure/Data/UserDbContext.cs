using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    
    public required DbSet<User> Users { get; set; }
    public required DbSet<Role> Roles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany() 
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Role>().HasKey(r => r.Id);

        base.OnModelCreating(modelBuilder);
    }
}