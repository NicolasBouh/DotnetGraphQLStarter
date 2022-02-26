using DotnetGraphQLStarter.Data;
using DotnetGraphQLStarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQLStarter.Features.Users;

public class UserByIdDataLoader : BatchDataLoader<int, User>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public UserByIdDataLoader(
        IBatchScheduler batchScheduler,
        IDbContextFactory<ApplicationDbContext> dbContextFactory)
        : base(batchScheduler)
    {
        _dbContextFactory = dbContextFactory ??
                            throw new ArgumentNullException(nameof(dbContextFactory));
    }

    protected override async Task<IReadOnlyDictionary<int, User>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using ApplicationDbContext dbContext =
            await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.Users
            .Where(s => keys.Contains(s.Id))
            .ToDictionaryAsync(t => t.Id, cancellationToken);
    }

    
}