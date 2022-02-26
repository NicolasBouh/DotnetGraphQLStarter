namespace DotnetGraphQLStarter.Features.Common;

public class BusinessError
{
    public BusinessError(string message, ErrorCode code)
    {
        Message = message;
        Code = code;
    }
    
    public BusinessError(string message, ErrorCode code, string field)
    {
        Message = message;
        Code = code;
        Field = field;
    }

    public string Message { get; }

    public ErrorCode Code { get; }
    
    public string? Field { get; }
}