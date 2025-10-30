using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Nix.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        // Только регистрируем open-generic репозиторий
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        return services;
    }

    public static IServiceCollection AddGenericUnitOfWork<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        // Регистрируем дженерик UoW с конкретным контекстом
        services.AddScoped<IUnitOfWork, GenericUnitOfWork<TContext>>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        return services;
    }
} 
