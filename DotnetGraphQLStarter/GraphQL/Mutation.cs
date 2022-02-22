using DotnetGraphQLStarter.Data;
using DotnetGraphQLStarter.Domain.Entities;
using DotnetGraphQLStarter.Extensions.GraphQL;
using DotnetGraphQLStarter.Users;

namespace DotnetGraphQLStarter.GraphQL;

public class Mutation
{
    [UseApplicationDbContext]
    public async Task<AddUserPayload> AddUserAsync(
        AddUserInput input,
        [ScopedService] ApplicationDbContext context)
    {
        var user = new User
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email,
            Password = input.Password
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new AddUserPayload(user);
    }
}