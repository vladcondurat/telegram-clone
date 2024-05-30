using Data.Entities;
using Data.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure.Context;

public interface IAppDbContext : IEntityFrameworkContext
{ 
    public DbSet<Message> Messages { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRoom> UserRooms { get; set; }
}