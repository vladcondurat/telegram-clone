using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Infrastructure.Repository;

public interface IEntityFrameworkContext : IDisposable
{
    IModel Model { get; }
    ChangeTracker ChangeTracker { get; }
    DatabaseFacade Database { get; }

    event EventHandler<SavingChangesEventArgs> SavingChanges;
    event EventHandler<SavedChangesEventArgs> SavedChanges;
    event EventHandler<SaveChangesFailedEventArgs> SaveChangesFailed;

    EntityEntry Add([NotNull] object entity);
    EntityEntry<TEntity> Add<TEntity>([NotNull] TEntity entity) where TEntity : class;
    ValueTask<EntityEntry> AddAsync([NotNull] object entity, CancellationToken cancellationToken = default);

    ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>([NotNull] TEntity entity,
        CancellationToken cancellationToken = default) where TEntity : class;

    void AddRange([NotNull] IEnumerable<object> entities);
    void AddRange([NotNull] params object[] entities);
    Task AddRangeAsync([NotNull] IEnumerable<object> entities, CancellationToken cancellationToken = default);
    Task AddRangeAsync([NotNull] params object[] entities);
    EntityEntry<TEntity> Attach<TEntity>([NotNull] TEntity entity) where TEntity : class;
    EntityEntry Attach([NotNull] object entity);
    void AttachRange([NotNull] params object[] entities);
    void AttachRange([NotNull] IEnumerable<object> entities);
    EntityEntry Entry([NotNull] object entity);
    EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;
    bool Equals(object obj);
    IQueryable<TResult> FromExpression<TResult>([NotNull] Expression<Func<IQueryable<TResult>>> expression);
    int GetHashCode();
    EntityEntry Remove([NotNull] object entity);
    EntityEntry<TEntity> Remove<TEntity>([NotNull] TEntity entity) where TEntity : class;
    void RemoveRange([NotNull] params object[] entities);
    void RemoveRange([NotNull] IEnumerable<object> entities);
    int SaveChanges(bool acceptAllChangesOnSuccess);
    int SaveChanges();
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<TEntity> Set<TEntity>([NotNull] string name) where TEntity : class;
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    string ToString();
    EntityEntry Update([NotNull] object entity);
    EntityEntry<TEntity> Update<TEntity>([NotNull] TEntity entity) where TEntity : class;
    void UpdateRange([NotNull] params object[] entities);
    void UpdateRange([NotNull] IEnumerable<object> entities);
}