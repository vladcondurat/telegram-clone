using Riok.Mapperly.Abstractions;
using Services.Features.Rooms;
using WebApi.Models;
using WebApi.Models.Room;

namespace WebApi.Mappers;

[Mapper]
public partial class RoomMapper
{
    public partial RoomModel RoomDtoToRoomModel(RoomDto roomDto);
    public partial RoomDto RoomModelToRoomDto(RoomModel roomModel);
    public partial CreateRoomDto CreateRoomModelToCreateRoomDto(CreateRoomModel createRoomModel);
    public partial CreateRoomModel CreateRoomDtoToCreateRoomModel(CreateRoomDto createRoomDto);
    public partial RoomCardModel RoomCardDtoToRoomCardModel(RoomCardDto roomCardDto);
    public partial RoomCardDto RoomCardModelToRoomCardDto(RoomCardModel roomCardModel);
    
    public partial UpdateRoomModel UpdateRoomDtoToUpdateRoomModel(UpdateRoomDto updateRoomDto);
    public partial UpdateRoomDto UpdateRoomModelToUpdateRoomDto(UpdateRoomModel updateRoomModel);
    
}