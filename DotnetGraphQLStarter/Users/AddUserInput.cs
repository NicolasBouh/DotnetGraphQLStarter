namespace DotnetGraphQLStarter.Users;

public record AddUserInput(
    string FirstName, 
    string LastName, 
    string Email, 
    string Password);