namespace Services.Features.Rooms;

public interface IRoomService
{
    RoomDto CreateRoom(CreateRoomDto dto, int userId);
    RoomDto GetRoomById(int roomId, int userId);
    GetRoomsDto GetRooms(int userId);
    RoomDto UpdateRoom(UpdateRoomDto dto, int roomId, int userId);
    void LeaveRoom(int roomId, int userId);
    void AddUsersToRoom(IEnumerable<int> userIdsToAdd, int roomId);
}