using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure.Context;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Message> Messages { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRoom> UserRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>().HasKey(m => m.Id);
        modelBuilder.Entity<Room>().HasKey(r => r.Id);
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<UserRoom>().HasKey(ur => new { ur.UserId, ur.RoomId });

        modelBuilder.Entity<Room>()
            .HasMany(r => r.Messages)
            .WithOne(m => m.Room)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Messages)
            .WithOne(m => m.User);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Rooms)
            .WithMany(r => r.Users)
            .UsingEntity<UserRoom>(
                j => j.HasOne(ur => ur.Room).WithMany(r => r.UserRooms).HasForeignKey(ur => ur.RoomId),
                j => j.HasOne(ur => ur.User).WithMany(u => u.UserRooms).HasForeignKey(ur => ur.UserId)
                ); 
        
        modelBuilder.Entity<User>().Property(x => x.Username).HasMaxLength(100);
        modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(100);
        modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(100);
        modelBuilder.Entity<User>().Property(x => x.AvatarUrl).HasMaxLength(1000);
        modelBuilder.Entity<Message>().Property(x => x.AttachedImageUrl).HasMaxLength(500);
        modelBuilder.Entity<Message>().Property(x => x.TextContent).HasMaxLength(10000);
        modelBuilder.Entity<Room>().Property(x => x.ImageUrl).HasMaxLength(1000);
        modelBuilder.Entity<Room>().Property(x => x.RoomName).HasMaxLength(100);

        base.OnModelCreating(modelBuilder);
    }

}