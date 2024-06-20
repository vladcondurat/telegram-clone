namespace WebApi.Models.Room;

public class GetRoomsModel
{
    public IEnumerable<RoomCardModel> Rooms { get; set; } = new List<RoomCardModel>();
}