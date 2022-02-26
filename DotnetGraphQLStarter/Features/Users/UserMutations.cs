using AutoMapper;
using DotnetGraphQLStarter.Data;
using DotnetGraphQLStarter.Domain.Entities;
using DotnetGraphQLStarter.Extensions.GraphQL;
using DotnetGraphQLStarter.Features.Common;
using DotnetGraphQLStarter.Features.Common.Interfaces;
using DotnetGraphQLStarter.Features.Users.Inputs;
using DotnetGraphQLStarter.Features.Users.Outputs;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQLStarter.Features.Users;

[ExtendObjectType("Mutation")]
public class UserMutations
{
    [UseApplicationDbContext]
    [UseAutoMapper]
    [UseUserService]
    public async Task<UserOutput> AddUserAsync(
        AddUserInput input,
        [ScopedService] ApplicationDbContext context,
        [ScopedService] IMapper mapper,
        [ScopedService] IUserService userService)
    {
        // Verify that an user don't exist with this email
        var existingUser = await context.Users
            .FirstOrDefaultAsync(x => x.Email == input.Email);

        if (existingUser != null)
            return new UserOutput(new BusinessError("User already exist", ErrorCode.USER_ALREADY_EXIST));
        
        // Encode Password
        var (passwordHash, passwordSalt) = userService.EncodePassword(input.Password);
        
        // Create user
        var user = new User
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email,
            PasswordHash = passwordHash,
            PassworSalt = passwordSalt
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserOutput(mapper.Map<UserDto>(user));
    }
    
    [UseApplicationDbContext]
    [UseUserService]
    public async Task<LoginOutput> LoginUserAsync(
        LoginUserInput input,
        [ScopedService] ApplicationDbContext context,
        [ScopedService] IUserService userService)
    {
        // Verify that an user exist
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Email == input.Email);

        if (user is null)
            return new LoginOutput(new BusinessError("Bad credentials", ErrorCode.USER_BAD_CREDENTIALS));
        
        // Verify that password match
        var match = userService.VerifyPassword(input.Password, user.PasswordHash, user.PassworSalt);
        
        if (!match) 
            return new LoginOutput(new BusinessError("Bad credentials", ErrorCode.USER_BAD_CREDENTIALS));
        
        // Encode new jwt token
        var token = userService.CreateToken(user.Id.ToString(), user.Email, user.FirstName, user.LastName);

        return new LoginOutput(token);
    }
}