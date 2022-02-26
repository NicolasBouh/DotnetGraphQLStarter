using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DotnetGraphQLStarter.Domain.Entities;
using DotnetGraphQLStarter.Features.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DotnetGraphQLStarter.Services;

public class UserService : IUserService
{
    private readonly SymmetricSecurityKey _key;
    private readonly IConfiguration _config;
    
    public UserService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
    }

    public string CreateToken(string userId, string email, string firstName, string lastName)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Name, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public (byte[] hash, byte[] salt) EncodePassword(string password)
    {
        using var hmac = new HMACSHA512();

        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var passwordSalt = hmac.Key;

        return (passwordHash, passwordSalt);
    }

    public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != passwordHash[i])
                return false;
        }

        return true;
    }
}