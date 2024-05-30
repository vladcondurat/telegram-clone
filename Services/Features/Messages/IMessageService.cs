namespace Services.Features.Messages;

public interface IMessageService
{
    MessageDto CreateMessage(MessageContentDto messageContentDto, int roomId, int userId);
    MessageDto UpdateMessage(MessageContentDto messageContentDto, int messageId, int userId);
    void DeleteMessage(int messageId, int userId);
}