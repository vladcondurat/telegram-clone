using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Features.Messages;
using Swashbuckle.AspNetCore.Annotations;
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
    [SwaggerOperation(Description = "Creates a new message with the specified details.")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MessageModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public IActionResult CreateMessage(MessageContentModel messageContentModel, int roomId)
    {
        ValidateUserId();
        var mapper = new MessageMapper();
        var messageContentDto = mapper.MessageContentModelToMessageContentDto(messageContentModel);
        var messageDto = _messageService.CreateMessage(messageContentDto, roomId, UserId!.Value);
        return StatusCode(StatusCodes.Status201Created,mapper.MessageDtoToMessageModel(messageDto));    
    } 

    [HttpPut("{messageId}")]
    [SwaggerOperation(Description = "Updates the details of a message.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public IActionResult UpdateMessage(MessageContentModel messageContentModel , int messageId)
    {
        ValidateUserId();
        var mapper = new MessageMapper();
        var messageContentDto = mapper.MessageContentModelToMessageContentDto(messageContentModel);
        var messageDto = _messageService.UpdateMessage(messageContentDto, messageId, UserId!.Value);
        return Ok(mapper.MessageDtoToMessageModel(messageDto));
    }
    
    [HttpDelete("{messageId}")]
    [SwaggerOperation(Description = "Deletes a message by its ID.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public IActionResult DeleteMessage(int messageId)
    {
        ValidateUserId();
        _messageService.DeleteMessage(messageId, UserId!.Value);
        return NoContent();
    }

}