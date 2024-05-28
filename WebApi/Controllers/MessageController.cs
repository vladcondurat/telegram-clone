using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Features.Messages;
using WebApi.Mappers;
using WebApi.Models;

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
    
    [HttpGet]
    public IActionResult GetMessageDetails(int? messageId)
    {
        var mapper = new MessageMapper();
        var messageDto = _messageService.GetMessageDetailsById(messageId);
        return Ok(mapper.MessageDtoToMessageModel(messageDto));
    }
    
    [HttpPut]
    public IActionResult UpdateMessage(CreateMessageModel model , int? messageId)
    {
        var mapper = new MessageMapper();
        var messageDto = _messageService.UpdateMessage(mapper.CreateMessageModelToCreateMessageDto(model), messageId, UserId);
        return Ok(mapper.MessageDtoToMessageModel(messageDto));
    }

    [HttpPost]
    public IActionResult CreateMessage(CreateMessageModel model, int roomId)
    {
        var mapper = new MessageMapper();
        var messageDto = _messageService.CreateMessage(mapper.CreateMessageModelToCreateMessageDto(model), UserId, roomId);
        return Ok(mapper.MessageDtoToMessageModel(messageDto));    
    } 
    
    [HttpDelete]
    public IActionResult DeleteMessage(int? messageId)
    {
        _messageService.DeleteMessage(messageId, UserId);
        return Ok();
    }

}