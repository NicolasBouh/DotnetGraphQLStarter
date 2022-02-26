using System.Reflection;
using AutoMapper;
using HotChocolate.Types.Descriptors;

namespace DotnetGraphQLStarter.Extensions.GraphQL;

public class UseAutoMapperAttribute : ObjectFieldDescriptorAttribute
{
    public override void OnConfigure(
        IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        MemberInfo member)
    {
        descriptor.UseAutoMapper<IMapper>();
    }
}