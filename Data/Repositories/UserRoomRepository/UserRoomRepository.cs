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
    
}