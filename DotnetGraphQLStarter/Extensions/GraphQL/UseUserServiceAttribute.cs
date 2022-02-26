using System.Reflection;
using DotnetGraphQLStarter.Features.Common.Interfaces;
using HotChocolate.Types.Descriptors;

namespace DotnetGraphQLStarter.Extensions.GraphQL;

public class UseUserServiceAttribute : ObjectFieldDescriptorAttribute
{
    public override void OnConfigure(
        IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        MemberInfo member)
    {
        descriptor.UseUserService<IUserService>();
    }
}