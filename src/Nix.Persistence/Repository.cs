using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Nix.Persistence;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
        
        // Диагностический лог для проверки версии DbContext
        if (System.Diagnostics.Debugger.IsAttached)
        {
            Console.WriteLine($"[DI DEBUG] Repo<T> sees DbContext assembly: {typeof(DbContext).Assembly.FullName}");
        }
    }

    public virtual async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync([id], cancellationToken);
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        return predicate == null
            ? await DbSet.CountAsync(cancellationToken)
            : await DbSet.CountAsync(predicate, cancellationToken);
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await DbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entityId = GetEntityId(entity);
        
        var isTracked = Context.Entry(entity).State != Microsoft.EntityFrameworkCore.EntityState.Detached;
        
        if (!isTracked)
        {
            var existingEntity = await DbSet.FindAsync([entityId], cancellationToken);
            
            if (existingEntity != null)
            {
                Context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                await DbSet.AddAsync(entity, cancellationToken);
            }
        }
        else
        {
            Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }

    /// <summary>
    /// Получает ID сущности для поиска при обновлении.
    /// Переопределите в наследниках для сложных ключей.
    /// </summary>
    protected virtual object GetEntityId(TEntity entity)
    {
        // Получаем первичный ключ через EF Core metadata
        var entityType = Context.Model.FindEntityType(typeof(TEntity));
        var key = entityType?.FindPrimaryKey();
        var keyProperty = key?.Properties.FirstOrDefault();
        
        if (keyProperty != null)
        {
            var propertyInfo = typeof(TEntity).GetProperty(keyProperty.Name);
            return propertyInfo?.GetValue(entity) ?? throw new InvalidOperationException("Cannot get entity ID");
        }
        
        throw new InvalidOperationException("Entity does not have a primary key defined");
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
    }

    public virtual void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }
} 
