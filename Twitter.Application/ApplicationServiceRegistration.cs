using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Twitter.Application.Services.Contract;
using Twitter.Application.Services.Implementation;

namespace Twitter.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<ITweetService, TweetService>();

        return services;
    }
}
