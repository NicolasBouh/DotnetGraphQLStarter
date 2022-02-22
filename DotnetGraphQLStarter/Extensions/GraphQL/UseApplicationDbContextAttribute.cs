using System.Reflection;
using DotnetGraphQLStarter.Data;
using HotChocolate.Types.Descriptors;

namespace DotnetGraphQLStarter.Extensions.GraphQL;

public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
{
    public override void OnConfigure(
        IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        MemberInfo member)
    {
        descriptor.UseDbContext<ApplicationDbContext>();
    }
}