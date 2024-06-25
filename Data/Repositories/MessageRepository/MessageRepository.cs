using Data.Entities;
using Data.Infrastructure.Context;
using Data.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.MessageRepository;

public class MessageRepository : Repository<Message>, IMessageRepository
{
    private readonly IAppDbContext _dbContext;

    public MessageRepository(IAppDbContext dbContext) : base((DbContext)dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Message? GetMessageByMessageId(int? id)
    {
        var message = _dbContext.Messages
            .AsNoTracking()
            .Include(m => m.User)
            .FirstOrDefault(m => m.Id == id);
        return message;
    }
}