namespace Services.Features.Rooms.Dtos;

public class GetRoomsDto
{
    public IEnumerable<RoomCardDto> Rooms { get; set; } = new List<RoomCardDto>();
}