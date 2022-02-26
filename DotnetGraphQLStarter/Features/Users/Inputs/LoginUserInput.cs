namespace DotnetGraphQLStarter.Features.Users.Inputs;

public record LoginUserInput(
    string Email, 
    string Password);