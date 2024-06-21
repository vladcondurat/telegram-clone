using Services.Features.Users;

namespace Services.Features.Rooms;

public interface IRoomService
{
    RoomDto CreateRoom(CreateRoomDto createRoomDto, int userId);
    RoomDto GetRoomById(int roomId, int userId);
    GetRoomsDto GetRooms(int userId);
    RoomDto UpdateRoom(UpdateRoomDto createRoomDto, int roomId, int userId);
    void AddUsersToRoom(IEnumerable<int> userIdsToAdd, int roomId);
    void LeaveRoom(int roomId, int userId);
    void RemoveUsersFromRoom(UserIdsDto userIds ,int roomId, int userId);
    
}