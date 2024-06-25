using Data.Entities;
using Data.Infrastructure.Repository;

namespace Data.Repositories.RoomRepository;

public interface IRoomRepository : IRepository<Room>
{
    Room? GetRoomByRoomId(int? roomId);
    Room? GetRoomWithMessagesByRoomId(int? roomId);
    Room? GetRoomByUserIds(int userId1, int userId2);
    IEnumerable<Room> GetRoomsByUserId(int? userId);
    
}