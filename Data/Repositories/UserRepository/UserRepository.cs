using Data.Entities;
using Data.Infrastructure.Context;
using Data.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.UserRepository;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly IAppDbContext _dbContext;

    public UserRepository(IAppDbContext dbContext) : base((DbContext)dbContext)
    {
        _dbContext = dbContext;
    }

    public User? GetUserByUserId(int id)
    {
        var user = _dbContext.Users
            .AsNoTracking()
            .FirstOrDefault(u => u.Id == id);
        return user;
    }
    
    public User? GetUserByUsername(string username)
    {
        var user = _dbContext.Users
            .AsNoTracking()
            .FirstOrDefault(u => u.Username == username);
        return user;
    }
    
    public IEnumerable<User> GetAllExceptCurrentUser(int id)
    {
        var users = _dbContext.Users
            .AsNoTracking()
            .Where(u => u.Id != id)
            .ToList();
        return users;
    }

}