using DotnetGraphQLStarter.Features.Common;

namespace DotnetGraphQLStarter.Features.Users.Outputs;

public class LoginOutput : Payload
{
    public string? Token { get; }

    public LoginOutput(string token)
    {
        Token = token;
    }
    
    public LoginOutput(IReadOnlyList<BusinessError>? errors = null) : base(errors)
    {
    }

    public LoginOutput(BusinessError error) : base(error)
    {
    }
}