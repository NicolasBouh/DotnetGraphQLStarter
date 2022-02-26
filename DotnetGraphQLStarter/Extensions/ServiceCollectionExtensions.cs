using System.Text;
using DotnetGraphQLStarter.Data;
using DotnetGraphQLStarter.Features.Common.Interfaces;
using DotnetGraphQLStarter.Features.Trainings;
using DotnetGraphQLStarter.Features.Users;
using DotnetGraphQLStarter.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DotnetGraphQLStarter.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddPooledDbContextFactory<ApplicationDbContext>(options => 
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void AddGraphQLayer(this IServiceCollection services)
    {
        services.AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<UserQueries>()
            .AddTypeExtension<TrainingQueries>()
            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<UserMutations>()
            .AddDataLoader<UserByIdDataLoader>()
            .AddDataLoader<TrainingByIdDataLoader>()
            .AddType<UserType>()
            .AddType<TrainingType>()
            .EnableRelaySupport();
    }

    public static void AddServicesLayer(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }

    public static void AddJwtAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey
                    };
            });
    }
    
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
    }
    
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}