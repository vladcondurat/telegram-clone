using Data.Entities;
using Riok.Mapperly.Abstractions;
using Services.Features.Messages;

namespace Services.Mappers;

[Mapper]
public partial class MessageMapper
{
    public partial MessageDto MessageToMessageDto(Message message);
}
