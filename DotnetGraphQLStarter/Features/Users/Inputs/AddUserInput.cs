namespace DotnetGraphQLStarter.Features.Users.Inputs;

public record AddUserInput(
    string FirstName, 
    string LastName, 
    string Email, 
    string Password);