using Data.Infrastructure.Context;
using Data.Repositories.MessageRepository;
using Data.Repositories.RoomRepository;
using Data.Repositories.UserRepository;
using Data.Repositories.UserRoomRepository;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IAppDbContext _dBContext;

    public UnitOfWork(
        IAppDbContext dBContext,
        IUserRepository userRepository,
        IMessageRepository messageRepository,
        IRoomRepository roomRepository,
        IUserRoomRepository userRoomRepository)
    {
        _dBContext = dBContext;
        Users = userRepository;
        Messages = messageRepository;
        Rooms = roomRepository;
        UserRooms = userRoomRepository;
    }

    public int SaveChanges()
    {
        return _dBContext.SaveChanges();
    }

    public void Dispose()
    {
        _dBContext.Dispose();
    }

    public void Reload<T>(T entity) where T : class
    {
        _dBContext.Entry(entity).Reload();
    }

    public bool IsModified<T>(T entity) where T : class
    {
        return _dBContext.Entry(entity).State == EntityState.Modified;
    }

    #region Repositories

    public IUserRepository Users { get; }
    public IMessageRepository Messages { get; }
    public IRoomRepository Rooms { get; }
    public IUserRoomRepository UserRooms { get; }

    #endregion
}