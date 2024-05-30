using Data.Entities;
using Data.Infrastructure.Repository;

namespace Data.Repositories.RoomRepository;

public interface IRoomRepository : IRepository<Room>
{
    Room? GetRoomByRoomId(int? roomId);
    Room? GetRoomWithMessagesByRoomId(int? roomId);
    IEnumerable<Room> GetRoomsByUserId(int? userId);
    
}