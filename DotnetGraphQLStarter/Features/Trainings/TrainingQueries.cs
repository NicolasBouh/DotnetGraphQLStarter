using DotnetGraphQLStarter.Data;
using DotnetGraphQLStarter.Domain.Entities;
using DotnetGraphQLStarter.Extensions.GraphQL;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQLStarter.Features.Trainings;

[ExtendObjectType("Query")]
public class TrainingQueries
{
    [UseApplicationDbContext]
    public Task<List<Training>> GetTrainings([ScopedService] ApplicationDbContext context) =>
        context.Trainings.ToListAsync();

    public Task<Training> GetTrainingAsync(
        [ID(nameof(Training))] int id,
        TrainingByIdDataLoader dataLoader,
        CancellationToken cancellationToken) =>
        dataLoader.LoadAsync(id, cancellationToken);
}