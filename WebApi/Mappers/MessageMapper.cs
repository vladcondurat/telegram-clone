using Riok.Mapperly.Abstractions;
using Services.Features.Messages;
using WebApi.Models;

namespace WebApi.Mappers;

[Mapper]
public partial class MessageMapper
{
    public partial MessageModel MessageDtoToMessageModel(MessageDto mapperDto);
    public partial CreateMessageDto CreateMessageModelToCreateMessageDto(CreateMessageModel mapperModel);
    public partial MessageDto MessageModelToMessageDto(MessageModel mapperModel);
}