using Microsoft.AspNetCore.Mvc;
using Services.Features.Rooms;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Models.Room;
using WebApi.Mappers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomController : ApplicationController
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        [SwaggerOperation(Description = "Creates a new room with the specified details.")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RoomModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult CreateRoom([FromBody] CreateRoomModel createRoomModel)
        {
            ValidateUserId();
            var mapper = new RoomMapper();
            var createRoomDto = mapper.CreateRoomModelToCreateRoomDto(createRoomModel);
            var roomDto = _roomService.CreateRoom(createRoomDto, UserId!.Value);
            return StatusCode(StatusCodes.Status201Created, mapper.RoomDtoToRoomModel(roomDto));
        }

        [HttpGet]
        [SwaggerOperation(Description = "Retrieves a list of all rooms.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomCardModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult GetRooms()
        {
            ValidateUserId();
            var mapper = new RoomMapper();
            var rooms = _roomService.GetRooms(UserId!.Value);
            var roomModels = mapper.GetRoomDtoToGetRoomModel(rooms);
            return Ok(roomModels);
        }

        [HttpGet("{roomId}")]
        [SwaggerOperation(Description = "Retrieves the details of a room by its ID.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult GetRoom(int roomId)
        {
            ValidateUserId();
            var mapper = new RoomMapper();
            var roomDto = _roomService.GetRoomById(roomId, UserId!.Value);
            var roomModel = mapper.RoomDtoToRoomModel(roomDto);
            return Ok(roomModel);
        }

        [HttpPut("{roomId}")]
        [SwaggerOperation(Description = "Updates the details of a room by its ID.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateRoomModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult UpdateRoom(UpdateRoomModel updateRoomModel, int roomId)
        {
            ValidateUserId();
            var mapper = new RoomMapper();
            var updateRoomDto = mapper.UpdateRoomModelToUpdateRoomDto(updateRoomModel);
            var roomDto = _roomService.UpdateRoom(updateRoomDto, roomId, UserId!.Value);
            return Ok(mapper.RoomDtoToRoomModel(roomDto));
        }

        [HttpDelete("{roomId}")]
        [SwaggerOperation(Description = "Leave a room by its ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult LeaveRoom(int roomId)
        {
            ValidateUserId();
            _roomService.LeaveRoom(roomId, UserId!.Value);
            return NoContent();
        }
        
        [HttpPost("add-users/{roomId}")]
        [SwaggerOperation(Description = "Adds users to a room specified by its ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult AddUsersToRoom(UserIdsModel userIdsModel, int roomId)
        {
            ValidateUserId();
            _roomService.AddUsersToRoom(userIdsModel.UserIds, roomId);
            return NoContent();
        }
        
        [HttpDelete("remove-users/{roomId}")]
        [SwaggerOperation(Description = "Removes users from a room specified by its ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult RemoveUsersFromRoom([FromQuery] UserIdsModel userIdsModel, int roomId)
        {
            ValidateUserId();
            var mapper = new UserMapper();
            var userIdsDto = mapper.UserIdsModelToUserIdsDto(userIdsModel);
            _roomService.RemoveUsersFromRoom(userIdsDto, roomId, UserId!.Value);
            return NoContent();
        }
    }
}

