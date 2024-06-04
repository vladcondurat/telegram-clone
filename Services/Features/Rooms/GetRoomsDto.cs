namespace Services.Features.Rooms;

public class GetRoomsDto
{
    public IEnumerable<RoomCardDto> Rooms { get; set; } = new List<RoomCardDto>();
}