using Data.Entities;
using Data.Infrastructure.Context;
using Data.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.RoomRepository;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    private readonly IAppDbContext _dbContext;

    public RoomRepository(IAppDbContext dbContext) : base((DbContext)dbContext)
    {
        _dbContext = dbContext;
    }

    public Room? GetRoomByRoomId(int? roomId)
    {
        var room = _dbContext.Rooms
            .AsNoTracking()
            .FirstOrDefault(r => r.Id == roomId);
        return room;
    }
    
    public Room? GetRoomWithMessagesByRoomId(int? roomId)
    {
        var room = _dbContext.Rooms
            .AsNoTracking()
            .Include(m => m.Messages)
            .ThenInclude(m => m.User)
            .FirstOrDefault(r => r.Id == roomId);
        return room;
    }
    
    public Room? GetRoomByUserIds(int userId1, int userId2)
    {
        var room = _dbContext.Rooms
            .AsNoTracking()
            .Include(r => r.Users)
            .FirstOrDefault(r => r.Users.Count() == 2 &&
                                 r.Users.Any(u => u.Id == userId1) &&
                                 r.Users.Any(u => u.Id == userId2));
        return room;
    }
    
    public Room? GetRoomByMessageId(int messageId)
    {
        var room = _dbContext.Rooms
            .AsNoTracking()
            .Include(r => r.Messages)
            .FirstOrDefault(r => r.Messages.Any(m => m.Id == messageId));
        return room;
    }
    
    public IEnumerable<Room> GetRoomsByUserId(int? userId)
    {
        var rooms = _dbContext.Rooms
            .AsNoTracking()
            .Include(r => r.Messages)
            .ThenInclude(m => m.User)
            .Where(r => r.Users.Any(u => u.Id == userId))
            .Select(r => new Room
                {
                    Id = r.Id,
                    ImageUrl = r.ImageUrl,
                    RoomName = r.RoomName,
                    IsGroup = r.IsGroup,
                    Messages = r.Messages.OrderByDescending(m => m.CreatedAt).Take(1)
                })
            .ToList();
        
        foreach (var room in rooms.Where(room => room.Messages.FirstOrDefault() is null))
        {
            room.Messages = new List<Message>();
        }

        return rooms;
    }

    public IEnumerable<int> GetRoomsIdByUserId(int? userId)
    {
        var roomsId = _dbContext.Rooms
            .AsNoTracking()
            .Where(r => r.Users.Any(u => u.Id == userId))
            .Select(r => r.Id)
            .ToList();
        return roomsId;
    }
}