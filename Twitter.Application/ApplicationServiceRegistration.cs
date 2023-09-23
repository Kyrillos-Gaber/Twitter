using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Twitter.Application.Services.Contract;
using Twitter.Application.Services.Implementation;
using Twitter.Infrastructure;
using Twitter.Infrastructure.Entities;

namespace Twitter.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<ITweetService, TweetService>();

        IdentityBuilder identityBuilder = services.AddIdentityCore<ApiUser>(
            opt => opt.User.RequireUniqueEmail = true);

        identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), services);

        identityBuilder.AddEntityFrameworkStores<AppIdentityDbContext>();

        services.AddScoped<IUserManagement, UserManagement>();

        return services;
    }

    public static IServiceCollection ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = Environment.GetEnvironmentVariable("KEY");

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
            };
        });

        return services;
    }
}
