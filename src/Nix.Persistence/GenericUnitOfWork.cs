using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;

namespace Nix.Persistence;

public class GenericUnitOfWork<TContext> : IUnitOfWork, IDisposable where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();
    private IDbContextTransaction? _transaction;
    private bool _disposed = false;

    public GenericUnitOfWork(TContext context)
    {
        _context = context;
        
        // Диагностический лог для проверки версии DbContext
        if (System.Diagnostics.Debugger.IsAttached)
        {
            Console.WriteLine($"[DI DEBUG] UoW<T> sees DbContext assembly: {typeof(DbContext).Assembly.FullName}");
            Console.WriteLine($"[DI DEBUG] UoW<T> context type: {typeof(TContext).FullName}");
        }
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);
        
        if (!_repositories.ContainsKey(type))
        {
            var repositoryInstance = new Repository<TEntity>(_context);
            _repositories[type] = repositoryInstance;
        }

        return (IRepository<TEntity>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    // Новые методы — обёртки под ExecutionStrategy + транзакция
    public async Task ExecuteInTransactionAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default)
    {
        var strategy = _context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var tx = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await action(cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await tx.CommitAsync(cancellationToken);
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }

    public async Task<TResult> ExecuteInTransactionAsync<TResult>(Func<CancellationToken, Task<TResult>> action, CancellationToken cancellationToken = default)
    {
        var strategy = _context.Database.CreateExecutionStrategy();
        return await strategy.ExecuteAsync(async () =>
        {
            await using var tx = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await action(cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await tx.CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }

    // Устаревшие методы транзакций
    [Obsolete("Use ExecuteInTransactionAsync(...) instead")]
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Use ExecuteInTransactionAsync(...) instead");
    }

    [Obsolete("Use ExecuteInTransactionAsync(...) instead")]
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Use ExecuteInTransactionAsync(...) instead");
    }

    [Obsolete("Use ExecuteInTransactionAsync(...) instead")]
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Use ExecuteInTransactionAsync(...) instead");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _transaction?.Dispose();
            _repositories.Clear();
            _disposed = true;
        }
    }
} 
