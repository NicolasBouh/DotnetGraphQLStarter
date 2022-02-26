using AutoMapper;
using DotnetGraphQLStarter.Domain.Entities;
using DotnetGraphQLStarter.Features.Users.Outputs;

namespace DotnetGraphQLStarter.Features.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile() => CreateMap<User, UserDto>();
}