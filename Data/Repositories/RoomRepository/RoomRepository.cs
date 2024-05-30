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
    
    public IEnumerable<Room> GetRoomsByUserId(int? userId)
    {
        var rooms = _dbContext.Rooms
            .AsNoTracking()
            .Include(r => r.Messages)
            .ThenInclude(m => m.User)
            .Where(r => r.Users.Any(u => u.Id == userId))
            .OrderByDescending(rm => rm.LastMessageTime)
            .Select(r => new Room
                {
                    Id = r.Id,
                    ImageUrl = r.ImageUrl,
                    RoomName = r.RoomName,
                    LastMessageTime = r.LastMessageTime,
                    Messages = r.Messages.OrderByDescending(m => m.CreatedAt).Take(1)
                })
            .ToList();
        
        foreach (var room in rooms)
        {
            if (room.Messages.FirstOrDefault() is null) room.Messages = new List<Message>();
        }

        return rooms;
    }

}