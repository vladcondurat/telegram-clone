using Data.Entities;
using Data.Infrastructure.Repository;

namespace Data.Repositories.MessageRepository;

public interface IMessageRepository : IRepository<Message>
{
    Message? GetMessageByMessageId(int? id);
}