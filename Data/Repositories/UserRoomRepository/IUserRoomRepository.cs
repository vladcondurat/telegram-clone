using Data.Entities;
using Data.Infrastructure.Repository;

namespace Data.Repositories.UserRoomRepository;

public interface IUserRoomRepository : IRepository<UserRoom>
{
    bool IsUserInRoom(int roomId, int userId); 
    UserRoom? GetUserRoomByUserIdAndRoomId(int userId, int roomId);
    IEnumerable<User> GetUsersInRoomExceptCurrent(int roomId, int userId);
    IEnumerable<User> GetUsersOutsideRoom(int roomId);
}