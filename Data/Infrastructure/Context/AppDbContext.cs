using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure.Context;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>().Property(x => x.Name).HasMaxLength(100);
        modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(100);

        base.OnModelCreating(modelBuilder);
    }
}