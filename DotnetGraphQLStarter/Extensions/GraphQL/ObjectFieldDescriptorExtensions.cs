using AutoMapper;
using DotnetGraphQLStarter.Features.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQLStarter.Extensions.GraphQL;

public static class ObjectFieldDescriptorExtensions
{
    public static IObjectFieldDescriptor UseDbContext<TDbContext>(
        this IObjectFieldDescriptor descriptor)
        where TDbContext : DbContext
    {
        return descriptor.UseScopedService<TDbContext>(
            create: s => s.GetRequiredService<IDbContextFactory<TDbContext>>().CreateDbContext(),
            disposeAsync: (s, c) => c.DisposeAsync());
    }
    
    public static IObjectFieldDescriptor UseAutoMapper<TMapper>(
        this IObjectFieldDescriptor descriptor)
        where TMapper : IMapper
    {
        return descriptor.UseScopedService<IMapper>(s => s.GetRequiredService<IMapper>());
    }
    
    public static IObjectFieldDescriptor UseUserService<TUserService>(
        this IObjectFieldDescriptor descriptor)
        where TUserService : IUserService
    {
        return descriptor.UseScopedService<IUserService>(s => s.GetRequiredService<IUserService>());
    }
    
    public static IObjectFieldDescriptor UseUpperCase(
        this IObjectFieldDescriptor descriptor)
    {
        return descriptor.Use(next => async context =>
        {
            await next(context);

            if (context.Result is string s)
            {
                context.Result = s.ToUpperInvariant();
            }
        });
    }
}