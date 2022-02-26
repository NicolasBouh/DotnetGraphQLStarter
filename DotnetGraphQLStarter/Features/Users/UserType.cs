using DotnetGraphQLStarter.Data;
using DotnetGraphQLStarter.Domain.Entities;
using DotnetGraphQLStarter.Extensions.GraphQL;
using DotnetGraphQLStarter.GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQLStarter.GraphQL.Types;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode((ctx, id) =>
                ctx.DataLoader<UserByIdDataLoader>().LoadAsync(id, ctx.RequestAborted)!);

        descriptor
            .Field(t => t.Trainings)
            .ResolveWith<UserResolvers>(t => t.GetTrainingsAsync(default!, default!, default!, default))
            .UseDbContext<ApplicationDbContext>()
            .Name("trainings");
    }

    private class UserResolvers
    {
        public async Task<IEnumerable<Training>> GetTrainingsAsync(
            User user,
            [ScopedService] ApplicationDbContext dbContext,
            TrainingByIdDataLoader trainingById,
            CancellationToken cancellationToken)
        {
            int[] trainingIds = await dbContext.Trainings
                .Where(s => s.UserId == user.Id)
                .Select(s => s.Id)
                .ToArrayAsync(cancellationToken);

            return await trainingById.LoadAsync(trainingIds, cancellationToken);
        }
    }
}