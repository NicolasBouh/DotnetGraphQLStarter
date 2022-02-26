using DotnetGraphQLStarter.Domain.Entities;
using DotnetGraphQLStarter.Features.Users;
using DotnetGraphQLStarter.GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQLStarter.GraphQL.Types;

public class TrainingType : ObjectType<Training>
{
    protected override void Configure(IObjectTypeDescriptor<Training> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode((ctx, id) =>
                ctx.DataLoader<TrainingByIdDataLoader>().LoadAsync(id, ctx.RequestAborted)!);
        
        
        descriptor
            .Field(t => t.User)
            .ResolveWith<TrainingResolvers>(t => t.GetUserAsync(default!, default!, default));
    }

    private class TrainingResolvers
    {
        public async Task<User?> GetUserAsync(
            Training training,
            UserByIdDataLoader userById,
            CancellationToken cancellationToken)
        {
            return await userById.LoadAsync(training.UserId, cancellationToken);
        }
    }
}