namespace DotnetGraphQLStarter.Features.Common;

public abstract class Payload
{
    protected Payload(IReadOnlyList<BusinessError>? errors = null)
    {
        Errors = errors;
    }
    
    protected Payload(BusinessError error)
    {
        Errors = new []{error};
    }

    public IReadOnlyList<BusinessError>? Errors { get; }
}