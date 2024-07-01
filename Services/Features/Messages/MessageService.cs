using Data.Contracts;
using Data.Entities;
using Data.Infrastructure.S3;
using Data.Infrastructure.UnitOfWork;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using Services.Constants;
using Services.Exceptions;
using Services.Features.Messages.Dtos;
using Services.Mappers;

namespace Services.Features.Messages;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IS3Handler _s3Handler;
    private readonly IPublishEndpoint _publishEndpoint;

    public MessageService(IUnitOfWork unitOfWork, IS3Handler s3Handler, IPublishEndpoint publishEndpoint)
    {
        _unitOfWork = unitOfWork;
        _s3Handler = s3Handler;
        _publishEndpoint = publishEndpoint;
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

        if (messageContentDto.TextContent.IsNullOrEmpty() && messageContentDto.AttachedImage is null)
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
            UserId = userId,
            RoomId = roomId,
        };
        
        if (messageContentDto.AttachedImage is not null)
        {
            message.AttachedImageUrl = _s3Handler.UploadFile(messageContentDto.AttachedImage);
        }
        
        if (!string.IsNullOrWhiteSpace(messageContentDto.TextContent))
        {
            message.TextContent = messageContentDto.TextContent;
        }
        
        room.LastActive = DateTime.Now;
        
        _unitOfWork.Rooms.Update(room);
        _unitOfWork.Messages.Add(message);
        _unitOfWork.SaveChanges();
        
        message.User = user;
        message.Room = room;
        
        var mapper = new MessageMapper();

        _publishEndpoint.Publish(new MessageCreated
        {
            RoomId = roomId.ToString(),
        });
        
        return mapper.MessageToMessageDto(message);
    }

    public MessageDto UpdateMessage(MessageContentDto messageContentDto, int messageId, int userId)
    {
        var message = _unitOfWork.Messages.GetMessageByMessageId(messageId);
        if (message is null)
        {
            throw new EntityNotFoundException(ErrorCodes.MessageNotFound,messageId, typeof(Message));
        }

        if (messageContentDto.TextContent.IsNullOrEmpty() && messageContentDto.AttachedImage is null)
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

        if (messageContentDto.AttachedImage is not null)
        {
            message.AttachedImageUrl = _s3Handler.UploadFile(messageContentDto.AttachedImage);
        }
        
        _unitOfWork.Messages.Update(message);
        _unitOfWork.SaveChanges();

        var mapper = new MessageMapper();
        
        var room = _unitOfWork.Rooms.GetRoomByMessageId(messageId);
        
        _publishEndpoint.Publish(new MessageCreated
        {
            RoomId = room!.Id.ToString(),
        });
        
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
        
        var room = _unitOfWork.Rooms.GetRoomByMessageId(messageId);
        
        _publishEndpoint.Publish(new MessageCreated
        {
            RoomId = room!.Id.ToString(),
        });
    }
    
}