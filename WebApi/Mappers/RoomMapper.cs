using Riok.Mapperly.Abstractions;
using Services.Features.Rooms.Dtos;
using WebApi.Models.Room;

namespace WebApi.Mappers;

[Mapper]
public partial class RoomMapper
{
    public partial RoomModel RoomDtoToRoomModel(RoomDto roomDto);
    public partial CreateRoomDto CreateRoomModelToCreateRoomDto(CreateRoomModel createRoomModel);
    public partial RoomCardModel RoomCardDtoToRoomCardModel(RoomCardDto roomCardDto);
    public partial UpdateRoomDto UpdateRoomModelToUpdateRoomDto(UpdateRoomModel updateRoomModel);
    public partial GetRoomsModel GetRoomDtoToGetRoomModel(GetRoomsDto getRoomsDto);
}