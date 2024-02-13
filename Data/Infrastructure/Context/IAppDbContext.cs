using Data.Entities;
using Data.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure.Context;

public interface IAppDbContext : IEntityFrameworkContext
{ 
    public DbSet<User> Users { get; set; }
}