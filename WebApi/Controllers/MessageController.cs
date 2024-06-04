using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Features.Messages;
using WebApi.Mappers;
using WebApi.Models.Message;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/messages")]
public class MessagesController : ApplicationController
{
    private readonly IMessageService _messageService;

    public MessagesController(IMessageService messageService)
    {
        _messageService = messageService;
    }
    
    [HttpPost("{roomId}")]
    public IActionResult CreateMessage(MessageContentModel messageContentModel, int roomId)
    {
        ValidateUserId();
        var mapper = new MessageMapper();
        var messageContentDto = mapper.MessageContentModelToMessageContentDto(messageContentModel);
        var messageDto = _messageService.CreateMessage(messageContentDto, roomId, UserId!.Value);
        return StatusCode(StatusCodes.Status201Created,mapper.MessageDtoToMessageModel(messageDto));    
    } 

    [HttpPut("{messageId}")]
    public IActionResult UpdateMessage(MessageContentModel messageContentModel , int messageId)
    {
        ValidateUserId();
        var mapper = new MessageMapper();
        var messageContentDto = mapper.MessageContentModelToMessageContentDto(messageContentModel);
        var messageDto = _messageService.UpdateMessage(messageContentDto, messageId, UserId!.Value);
        return Ok(mapper.MessageDtoToMessageModel(messageDto));
    }
    
    [HttpDelete("{messageId}")]
    public IActionResult DeleteMessage(int messageId)
    {
        ValidateUserId();
        _messageService.DeleteMessage(messageId, UserId!.Value);
        return NoContent();
    }

}