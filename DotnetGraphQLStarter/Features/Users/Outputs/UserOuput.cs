using DotnetGraphQLStarter.Features.Common;

namespace DotnetGraphQLStarter.Features.Users.Outputs;

public class UserOutput : Payload
{
    public UserDto? User { get; }

    public UserOutput(UserDto user)
    {
        User = user;
    }
    
    public UserOutput(IReadOnlyList<BusinessError> errors)
        : base(errors)
    {
    }
    
    public UserOutput(BusinessError error)
        : base(error)
    {
    }
}