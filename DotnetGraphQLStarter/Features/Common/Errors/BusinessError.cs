namespace DotnetGraphQLStarter.Features.Common;

public class BusinessError
{
    public BusinessError(string message, string code)
    {
        Message = message;
        Code = code;
    }

    public string Message { get; }

    public string Code { get; }
}