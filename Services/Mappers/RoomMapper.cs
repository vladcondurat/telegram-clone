using Data.Entities;
using Riok.Mapperly.Abstractions;
using Services.Features.Rooms;
using Services.Features.Rooms.Dtos;

namespace Services.Mappers;

[Mapper]
public partial class RoomMapper
{
    public partial RoomDto RoomToRoomDto(Room room);
    public partial RoomCardDto RoomToRoomCardDto(Room room);
}