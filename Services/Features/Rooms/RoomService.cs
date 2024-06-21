using Data.Constants;
using Data.Entities;
using Data.Infrastructure.S3;
using Data.Infrastructure.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using Services.Constants;
using Services.Exceptions;
using Services.Features.Users;
using Services.Mappers;

namespace Services.Features.Rooms;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IS3Handler _s3Handler;
    
    public RoomService (IUnitOfWork unitOfWork, IS3Handler s3Handler)
    {
        _unitOfWork = unitOfWork;
        _s3Handler = s3Handler;
    }

    public RoomDto CreateRoom(CreateRoomDto createRoomDto, int userId)
    {
        var userIdsList = new List<int> { userId };
        userIdsList.AddRange(createRoomDto.UserIds);
        
        foreach (var id in userIdsList)
        {
            var existingUser = _unitOfWork.Users.GetUserByUserId(id);
            if (existingUser is null)
            {
                throw new EntityNotFoundException(id, typeof(User));
            }
        }

        if (userIdsList.Distinct().Count() < 2)
        {
            throw new BusinessException(ErrorCodes.RoomMinUsers,"Room must have at least 2 users");
        }

        if (string.IsNullOrWhiteSpace(createRoomDto.RoomName))
        {
            createRoomDto.RoomName = Defaults.DefaultRoomName;
        }
        
        if (string.IsNullOrWhiteSpace(createRoomDto.ImageUrl))
        {
            createRoomDto.ImageUrl = Defaults.DefaultRoomImage;
        }

        var room = new Room
        {
            ImageUrl = createRoomDto.ImageUrl,
            RoomName = createRoomDto.RoomName,
        };

        _unitOfWork.Rooms.Add(room);
        _unitOfWork.SaveChanges();
        
        AddUsersToRoom(userIdsList, room.Id);
        
        var mapper = new RoomMapper();
        return mapper.RoomToRoomDto(room);
    }

    public RoomDto GetRoomById(int roomId, int userId)
    {
        var room = _unitOfWork.Rooms.GetRoomWithMessagesByRoomId(roomId);
        if (room is null)
        {
            throw new EntityNotFoundException(ErrorCodes.RoomNotFound, roomId, typeof(Room));
        }
        
        var isUserInRoom = _unitOfWork.UserRooms.IsUserInRoom(roomId, userId);
        if (!isUserInRoom)
        {
            throw new AuthorizationException();
        }
        
        var mapper = new RoomMapper();
        return mapper.RoomToRoomDto(room);
    }
    
    public GetRoomsDto GetRooms(int userId)
    {
        var user = _unitOfWork.Users.GetUserByUserId(userId);
        if (user is null)
        {
            throw new EntityNotFoundException(ErrorCodes.UserNotFound,userId, typeof(User)); 
        }
        
        var rooms = _unitOfWork.Rooms.GetRoomsByUserId(userId);
        
        var roomMapper = new RoomMapper();
        var messageMapper = new MessageMapper();
        var roomsDto = rooms.Select(room =>
        {
            var roomCardDto = roomMapper.RoomToRoomCardDto(room);
            if (room.Messages.Any())
            {
                roomCardDto.LastMessage = messageMapper.MessageToMessageDto(room.Messages.ToList()[0]);
            }
 
            return roomCardDto;
        }).OrderByDescending(r => r.LastMessage?.CreatedAt).ToList();
        
        var getRoomsDto = new GetRoomsDto
        {
            Rooms = roomsDto
        };
        
        return getRoomsDto;
    }
    
    public RoomDto UpdateRoom(UpdateRoomDto dto, int roomId, int userId)
    {
        var room = _unitOfWork.Rooms.GetRoomByRoomId(roomId);
        if (room is null)
        {
            throw new EntityNotFoundException(ErrorCodes.RoomNotFound,roomId, typeof(Room));
        }
        
        var isUserInRoom = _unitOfWork.UserRooms.IsUserInRoom(roomId, userId);
        if (!isUserInRoom)
        {
            throw new AuthorizationException();
        }
        
        if (dto.ImageUrl != null)
        {
            var url = _s3Handler.UploadFile(dto.ImageUrl);
            room.ImageUrl = url;
        }

        if (!dto.RoomName.IsNullOrEmpty())
        {
            room.RoomName = dto.RoomName!;
        }

        _unitOfWork.Rooms.Update(room);
        _unitOfWork.SaveChanges();
        
        var mapper = new RoomMapper();
        return mapper.RoomToRoomDto(room);
    }
    
    public void AddUsersToRoom(IEnumerable<int> userIdsToAdd, int roomId)
    {
        var existingRoom = _unitOfWork.Rooms.GetRoomByRoomId(roomId);
        if (existingRoom is null)
        {
            throw new EntityNotFoundException(ErrorCodes.RoomNotFound, roomId, typeof(Room));
        }
        
        var newUserRooms = new List<UserRoom>();
        
        foreach (var userId in userIdsToAdd)
        {
            var existingUser = _unitOfWork.Users.GetUserByUserId(userId);
            if (existingUser is null)
            {
                throw new EntityNotFoundException(userId, typeof(User));
            }

            if (_unitOfWork.UserRooms.IsUserInRoom(roomId, userId) || newUserRooms.Any(ur => ur.UserId == userId))
            {
                continue;
            }
            
            var newUserRoom = new UserRoom()
            {
                RoomId = roomId,
                UserId = existingUser.Id
            };
            newUserRooms.Add(newUserRoom);
        }
        
        _unitOfWork.UserRooms.AddRange(newUserRooms);
        _unitOfWork.SaveChanges(); 
    }
    
    public void LeaveRoom(int roomId, int userId)
    {
        var userRoom = _unitOfWork.UserRooms.GetUserRoomByUserIdAndRoomId(userId, roomId);
        if (userRoom is null)
        {
            throw new AuthorizationException();
        }
        
        _unitOfWork.UserRooms.Delete(userRoom);
        _unitOfWork.SaveChanges();
    } 
    
    public void RemoveUsersFromRoom(UserIdsDto userIdsDto, int roomId, int userId)
    {
        var isUserInRoom = _unitOfWork.UserRooms.IsUserInRoom(roomId, userId);
        if (!isUserInRoom)
        {
            throw new AuthorizationException();
        }
        
        var userRooms = _unitOfWork.UserRooms.GetUserRoomsByRoomId(roomId);
        var userRoomsToRemove = userRooms.Where(ur => userIdsDto.UserIds.Contains(ur.UserId)).ToList();
        
        foreach (var userRoom in userRoomsToRemove)
        {
            _unitOfWork.UserRooms.Delete(userRoom);
        }
        
        _unitOfWork.SaveChanges();
    }
}
