using DotnetGraphQLStarter.Domain.Entities;

namespace DotnetGraphQLStarter.Users;

public class AddUserPayload
{
    public User User { get; }

    public AddUserPayload(User user)
    {
        User = user;
    }
}