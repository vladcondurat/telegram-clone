using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Features.Rooms;
using WebApi.Models.Room;
using WebApi.Mappers;

namespace WebApi.Controllers;

[ApiController]
[Route("api/rooms")]
[Authorize]
public class RoomController : ApplicationController
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }
    
    [HttpPost]
    public IActionResult CreateRoom(CreateRoomModel createRoomModel)
    {
        ValidateUserId();
        var mapper = new RoomMapper();
        var room = _roomService.CreateRoom(mapper.CreateRoomModelToCreateRoomDto(createRoomModel), UserId!.Value);
        return StatusCode(StatusCodes.Status201Created, mapper.RoomDtoToRoomModel(room));
    }
    
    [HttpGet]
    public IActionResult GetLatestRooms()
    {
        ValidateUserId();
        var mapper = new RoomMapper();
        var rooms = _roomService.GetRooms(UserId!.Value);
        var roomModels = rooms.Select(r => mapper.RoomCardDtoToRoomCardModel(r)).ToList();
        return Ok(roomModels);
    }
    
    [HttpGet("details")]
    public IActionResult GetRoomDetails([Required] int roomId)
    {
        ValidateUserId();
        var mapper = new RoomMapper();
        var roomDto = _roomService.GetRoomDetailsById(roomId, UserId!.Value);
        return Ok(mapper.RoomDtoToRoomModel(roomDto));
    }
    
    [HttpPut]
    public IActionResult UpdateRoom(UpdateRoomModel updateRoomModel, [Required] int roomId)
    {
        ValidateUserId();
        var mapper = new RoomMapper();
        var room = _roomService.UpdateRoom(mapper.UpdateRoomModelToUpdateRoomDto(updateRoomModel), roomId, UserId!.Value);
        return Ok(mapper.UpdateRoomDtoToUpdateRoomModel(room));
    }
    
    [HttpDelete]
    public IActionResult DeleteRoom([Required] int roomId)
    {
        ValidateUserId();
        _roomService.DeleteRoom(roomId, UserId!.Value);
        return Ok();
    }
}

//la update, create ce Dto returnez? RoomDto cu toate sau depinde de la caz la caz?