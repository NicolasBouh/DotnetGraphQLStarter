using DotnetGraphQLStarter.Data;
using DotnetGraphQLStarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQLStarter.Features.Trainings;

public class TrainingByIdDataLoader : BatchDataLoader<int, Training>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public TrainingByIdDataLoader(
        IBatchScheduler batchScheduler,
        IDbContextFactory<ApplicationDbContext> dbContextFactory)
        : base(batchScheduler)
    {
        _dbContextFactory = dbContextFactory ??
                            throw new ArgumentNullException(nameof(dbContextFactory));
    }

    protected override async Task<IReadOnlyDictionary<int, Training>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using ApplicationDbContext dbContext =
            await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.Trainings
            .Where(s => keys.Contains(s.Id))
            .ToDictionaryAsync(t => t.Id, cancellationToken);
    }

    
}