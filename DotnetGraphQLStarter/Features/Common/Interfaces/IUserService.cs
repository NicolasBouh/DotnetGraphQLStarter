namespace DotnetGraphQLStarter.Features.Common.Interfaces;

public interface IUserService
{
    string CreateToken(string userId, string email, string firstName, string lastName);

    (byte[] hash, byte[] salt) EncodePassword(string password);
    
    bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
}