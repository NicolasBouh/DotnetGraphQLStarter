using DotnetGraphQLStarter.Data;
using DotnetGraphQLStarter.Domain.Entities;
using DotnetGraphQLStarter.Extensions.GraphQL;
using DotnetGraphQLStarter.GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQLStarter.GraphQL;

public class Query
{
    [UseApplicationDbContext]
    public Task<List<Training>> GetTrainings([ScopedService] ApplicationDbContext context) =>
        context.Trainings.ToListAsync();
    
    public Task<Training> GetTrainingAsync(int id, TrainingByIdDataLoader dataLoader, CancellationToken cancellationToken) =>
        dataLoader.LoadAsync(id, cancellationToken);
    
    [UseApplicationDbContext]
    public Task<List<User>> GetUsers([ScopedService] ApplicationDbContext context) =>
        context.Users.ToListAsync();

    public Task<User> GetUserAsync(int id, UserByIdDataLoader dataLoader, CancellationToken cancellationToken) =>
        dataLoader.LoadAsync(id, cancellationToken);
}