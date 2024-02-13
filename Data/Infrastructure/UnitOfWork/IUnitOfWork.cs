using Data.Repositories.UserRepository;

namespace Data.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    public IUserRepository Users { get; }

    int SaveChanges();
    void Reload<T>(T entity) where T : class;
    bool IsModified<T>(T entity) where T : class;
    void Dispose();
}