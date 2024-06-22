using Riok.Mapperly.Abstractions;
using Services.Features.Messages.Dtos;
using WebApi.Models.Message;

namespace WebApi.Mappers;

[Mapper]
public partial class MessageMapper
{
    public partial MessageModel MessageDtoToMessageModel(MessageDto mapperDto);
    public partial MessageContentDto MessageContentModelToMessageContentDto(MessageContentModel mapperContentModel);
}