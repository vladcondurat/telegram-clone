using Data.Infrastructure.Context;
using Data.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IAppDbContext _dBContext;

    public UnitOfWork(
        IAppDbContext dBContext,
        IUserRepository userRepository)
    {
        _dBContext = dBContext;
        Users = userRepository;
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

    #endregion
}