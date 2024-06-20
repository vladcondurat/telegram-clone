using Data.Entities;
using Data.Infrastructure.Context;
using Data.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.UserRoomRepository;

public class UserRoomRepository : Repository<UserRoom>, IUserRoomRepository
{
    private readonly IAppDbContext _dbContext;

    public UserRoomRepository(IAppDbContext dbContext) : base((DbContext)dbContext)
    {
        _dbContext = dbContext;
    }

    public bool IsUserInRoom(int roomId, int userId)
    {
        var isUserInRoom = _dbContext.UserRooms
            .AsNoTracking()
            .Any(ur => ur.User.Id == userId && ur.Room.Id == roomId);
        return isUserInRoom;
    }
    
    public UserRoom? GetUserRoomByUserIdAndRoomId(int userId, int roomId)
    {
        var userRoom = _dbContext.UserRooms
            .AsNoTracking()
            .FirstOrDefault(ur => ur.User.Id == userId && ur.Room.Id == roomId);
        return userRoom;
    }
    
    public IEnumerable<User> GetUsersInRoomExceptCurrent(int roomId, int userId)
    {
        var users = _dbContext.UserRooms
            .AsNoTracking()
            .Include(ur => ur.User)
            .Where(ur => ur.Room.Id == roomId && ur.UserId != userId)
            .Select(ur => ur.User)
            .ToList();
        return users;
    }

    public IEnumerable<User> GetUsersOutsideRoom(int roomId)
    {
        var usersInRoom = _dbContext.UserRooms
            .AsNoTracking()
            .Where(ur => ur.Room.Id == roomId)
            .Select(ur => ur.UserId)
            .ToList();
        
        return _dbContext.Users
            .Where(u => !usersInRoom.Contains(u.Id))
            .ToList();
    }
}