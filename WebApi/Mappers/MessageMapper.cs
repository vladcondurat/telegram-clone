using Riok.Mapperly.Abstractions;
using Services.Features.Messages;
using WebApi.Models;
using WebApi.Models.Messages;

namespace WebApi.Mappers;

[Mapper]
public partial class MessageMapper
{
    public partial MessageModel MessageDtoToMessageModel(MessageDto mapperDto);
    public partial MessageContentDto MessageContentModelToMessageContentDto(MessageContentModel mapperContentModel);
}