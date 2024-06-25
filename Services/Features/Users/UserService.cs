using Data.Entities;
using Data.Infrastructure.S3;
using Data.Infrastructure.UnitOfWork;
using Services.Constants;
using Services.Exceptions;
using Services.Features.Users.Dtos;
using Services.Mappers;

namespace Services.Features.Users;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IS3Handler _s3Handler;

    public UserService(IUnitOfWork unitOfWork, IS3Handler s3Handler)
    {
        _unitOfWork = unitOfWork;
        _s3Handler = s3Handler;
    }
    
    public UserDto GetUser(int id)
    {
        var user = _unitOfWork.Users.GetUserByUserId(id);

        if (user is null)
        {
            throw new EntityNotFoundException(ErrorCodes.UserNotFound,id, typeof(User));
        }
        
        var mapper = new UserMapper();
        return mapper.UserToUserDto(user);
    }

    public GetUsersDto GetUsers(int id)
    {
        var existingUser = _unitOfWork.Users.GetUserByUserId(id);

        if (existingUser is null)
        {
            throw new EntityNotFoundException(ErrorCodes.UserNotFound,id, typeof(User));
        }
        
        var users = _unitOfWork.Users.GetAllExceptCurrentUser(id);
        
        var mapper = new UserMapper();
        var usersDto = users.Select(user => mapper.UserToUserDto(user)).ToList();
        
        var getUsersDto = new GetUsersDto
        {
            Users = usersDto
        };
        
        return getUsersDto;
    }

    public GetUsersDto GetUsersInRoom(int roomId, int userId)
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
        
        var users = _unitOfWork.UserRooms.GetUsersInRoomExceptCurrent(roomId, userId);
        
        var mapper = new UserMapper();
        var usersDto = users.Select(user => mapper.UserToUserDto(user)).ToList();
        
        var getUsersDto = new GetUsersDto
        {
            Users = usersDto
        };
        
        return getUsersDto;
    }
    
    public GetUsersDto GetUsersOutsideRoom(int roomId, int userId)
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
        
        var users = _unitOfWork.UserRooms.GetUsersOutsideRoom(roomId);
        
        var mapper = new UserMapper();
        var usersDto = users.Select(user => mapper.UserToUserDto(user)).ToList();
        
        var getUsersDto = new GetUsersDto
        {
            Users = usersDto
        };
        
        return getUsersDto;
    }
    
    public UserDto UpdateUser(UpdateUserDto updateUserDto, int id)
    {
        var existingUser = _unitOfWork.Users.GetUserByUserId(id);

        if (existingUser is null)
        {
            throw new EntityNotFoundException(ErrorCodes.UserNotFound,id, typeof(User));
        }
        
        if (!string.IsNullOrWhiteSpace(updateUserDto.Email))
        {
            existingUser.Email = updateUserDto.Email;
        }
        
        if (!string.IsNullOrWhiteSpace(updateUserDto.Username))
        {
            var userWithSameUsername = _unitOfWork.Users.GetUserByUsername(updateUserDto.Username);

            if(userWithSameUsername is not null && userWithSameUsername.Id != id)
            {
                throw new BusinessException(ErrorCodes.UsernameAlreadyExists,
                    "Username already in use");
            }
            
            existingUser.Username = updateUserDto.Username;
        }
        
        if (updateUserDto.AvatarImg is not null)
        {
            existingUser.AvatarUrl = _s3Handler.UploadFile(updateUserDto.AvatarImg);
        }
        
        _unitOfWork.Users.Update(existingUser);
        _unitOfWork.SaveChanges();
        
        var mapper = new UserMapper();
        return mapper.UserToUserDto(existingUser);
    }
    
    public void UpdateUserLastActive(int userId)
    {
        var user = _unitOfWork.Users.GetUserByUserId(userId);
        if (user is null)
        {
            throw new EntityNotFoundException(ErrorCodes.UserNotFound,userId, typeof(User));
        }
        
        user.LastActive = DateTime.UtcNow;
        _unitOfWork.Users.Update(user);
        _unitOfWork.SaveChanges();
        _unitOfWork.Detach(user);
    }
}