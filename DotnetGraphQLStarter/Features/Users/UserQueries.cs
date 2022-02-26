using System.Security.Claims;
using DotnetGraphQLStarter.Data;
using DotnetGraphQLStarter.Domain.Entities;
using DotnetGraphQLStarter.Extensions.GraphQL;
using DotnetGraphQLStarter.Features.Users.Outputs;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQLStarter.Features.Users;

[ExtendObjectType("Query")]
public class UserQueries
{
    [UseApplicationDbContext]
    public async Task<List<UserDto>> GetUsers(
        [ScopedService] ApplicationDbContext 
        dbContext, IResolverContext context) =>
            await dbContext.Users.ProjectTo<User, UserDto>(context).ToListAsync();
    
    [UseApplicationDbContext]
    public async Task<UserDto?> GetUserAsync(
        [ID(nameof(User))]int id,
        [ScopedService] ApplicationDbContext dbContext, 
        IResolverContext context) =>
            await dbContext.Users.ProjectTo<User, UserDto>(context)
                .FirstOrDefaultAsync(x => x.Id == id);
    
    [Authorize]
    [UseApplicationDbContext]
    public async Task<UserDto?> GetCurrentUserAsync(
        [ScopedService] ApplicationDbContext dbContext, 
        ClaimsPrincipal claimsPrincipal,
        IResolverContext context) {
        var userId = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
        
        return await dbContext.Users.ProjectTo<User, UserDto>(context)
            .FirstOrDefaultAsync(x => x.Id == userId);
    }
}