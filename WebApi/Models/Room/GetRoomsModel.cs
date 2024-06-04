namespace WebApi.Models.Room;

public class GetRoomsModel
{
    public IEnumerable<RoomModel> Rooms { get; set; } = new List<RoomModel>();
}