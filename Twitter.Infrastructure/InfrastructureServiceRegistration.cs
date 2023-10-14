using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Twitter.Infrastructure.Repository.Contract;
using Twitter.Infrastructure.Repository.Implementation;

namespace Twitter.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DBConnection")));

        services.AddScoped<ITweetRepository, TweetRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        

        return services;
    }
}
