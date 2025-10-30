using System.Linq.Expressions;

namespace Nix.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    
    void Update(TEntity entity);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    void UpdateRange(IEnumerable<TEntity> entities);
    
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
}

public interface IUnitOfWork
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    // Новые методы — обёртки под ExecutionStrategy + транзакция
    Task ExecuteInTransactionAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default);
    Task<TResult> ExecuteInTransactionAsync<TResult>(Func<CancellationToken, Task<TResult>> action, CancellationToken cancellationToken = default);
    
    // Устаревшие методы транзакций
    [Obsolete("Use ExecuteInTransactionAsync(...) instead")]
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    [Obsolete("Use ExecuteInTransactionAsync(...) instead")]
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    [Obsolete("Use ExecuteInTransactionAsync(...) instead")]
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
} 
