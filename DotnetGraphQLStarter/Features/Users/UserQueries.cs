using DotnetGraphQLStarter.Data;
using DotnetGraphQLStarter.Domain.Entities;
using DotnetGraphQLStarter.Extensions.GraphQL;
using DotnetGraphQLStarter.GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQLStarter.GraphQL.Queries;

[ExtendObjectType("Query")]
public class UserQueries
{
    [UseApplicationDbContext]
    public Task<List<User>> GetUsers([ScopedService] ApplicationDbContext context) =>
        context.Users.ToListAsync();

    public Task<User> GetUserAsync(
        [ID(nameof(User))]int id,
        UserByIdDataLoader dataLoader,
        CancellationToken cancellationToken) =>
        dataLoader.LoadAsync(id, cancellationToken);
}