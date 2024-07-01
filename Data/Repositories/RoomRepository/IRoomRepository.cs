using Data.Entities;
using Data.Infrastructure.Repository;

namespace Data.Repositories.RoomRepository;

public interface IRoomRepository : IRepository<Room>
{
    Room? GetRoomByRoomId(int? roomId);
    Room? GetRoomWithMessagesByRoomId(int? roomId);
    Room? GetRoomByUserIds(int userId1, int userId2);
    Room? GetRoomByMessageId(int messageId);
    IEnumerable<Room> GetRoomsByUserId(int? userId);
    IEnumerable<int> GetRoomsIdByUserId(int? userId);
    
}