using Data.Entities;
using Data.Infrastructure.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using Services.Constants;
using Services.Exceptions;
using Services.Mappers;

namespace Services.Features.Messages;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;

    public MessageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public MessageDto CreateMessage(MessageContentDto messageContentDto, int roomId, int userId)
    {
        var user = _unitOfWork.Users.GetUserByUserId(userId);
        if (user is null)
        {
            throw new BusinessException(ErrorCodes.UserNotFound, "User not found");
        }

        var room = _unitOfWork.Rooms.GetRoomByRoomId(roomId);
        if (room is null)
        {
            throw new BusinessException(ErrorCodes.RoomNotFound, "Room not found");
        }

        if (messageContentDto.TextContent.IsNullOrEmpty() && messageContentDto.AttachedImageUrl.IsNullOrEmpty())
        {
            throw new BusinessException(ErrorCodes.MessageEmpty, "You can not send an empty message");
        }

        var isUserInRoom = _unitOfWork.UserRooms.IsUserInRoom(roomId, userId);
        if (!isUserInRoom)
        {
            throw new AuthorizationException();
        }

        var message = new Message
        {
            TextContent = messageContentDto.TextContent,
            AttachedImageUrl = messageContentDto.AttachedImageUrl,
            UserId = userId,
            RoomId = roomId,
        };
        
        _unitOfWork.Messages.Add(message);
        _unitOfWork.SaveChanges();
        
        message.User = user;
        message.Room = room;

        var mapper = new MessageMapper();
        return mapper.MessageToMessageDto(message);
    }

    public MessageDto UpdateMessage(MessageContentDto messageContentDto, int messageId, int userId)
    {
        var message = _unitOfWork.Messages.GetMessageByMessageId(messageId);
        if (message is null)
        {
            throw new EntityNotFoundException(ErrorCodes.MessageNotFound,messageId, typeof(Message));
        }

        if (messageContentDto.TextContent.IsNullOrEmpty() && messageContentDto.AttachedImageUrl.IsNullOrEmpty())
        {
            throw new BusinessException(ErrorCodes.MessageEmpty,"You can not send an empty message");
        }

        if (userId != message.UserId)
        {
            throw new AuthorizationException();
        }
        if (!messageContentDto.TextContent.IsNullOrEmpty())
        {
            message.TextContent = messageContentDto.TextContent;
        }

        if (!messageContentDto.AttachedImageUrl.IsNullOrEmpty())
        {
            message.AttachedImageUrl = messageContentDto.AttachedImageUrl;
        }
        
        _unitOfWork.Messages.Update(message);
        _unitOfWork.SaveChanges();

        var mapper = new MessageMapper();
        return mapper.MessageToMessageDto(message);
    }
    
    public void DeleteMessage(int messageId, int userId)
    {
        var message = _unitOfWork.Messages.GetMessageByMessageId(messageId);
        if (message is null)
        {
            throw new EntityNotFoundException(ErrorCodes.MessageNotFound,messageId, typeof(Message));
        }

        if (userId != message.UserId)
        {
            throw new AuthorizationException();
        }
        
        _unitOfWork.Messages.Delete(message);
        _unitOfWork.SaveChanges();
    }

}