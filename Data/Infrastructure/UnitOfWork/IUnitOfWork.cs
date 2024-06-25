using Data.Repositories.MessageRepository;
using Data.Repositories.RoomRepository;
using Data.Repositories.UserRepository;
using Data.Repositories.UserRoomRepository;

namespace Data.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    public IUserRepository Users { get; }
    public IMessageRepository Messages { get; }
    public IRoomRepository Rooms { get; }
    public IUserRoomRepository UserRooms { get; }

    int SaveChanges();
    void Reload<T>(T entity) where T : class;
    bool IsModified<T>(T entity) where T : class;
    void Detach<T>(T entity) where T : class;
    void Dispose();
}