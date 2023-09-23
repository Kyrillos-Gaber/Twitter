using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Twitter.Infrastructure.Configurations;
using Twitter.Infrastructure.Entities;

namespace Twitter.Infrastructure;

public class AppIdentityDbContext : IdentityDbContext<ApiUser>
{
    public AppIdentityDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Tweet> Tweets { get; set; }

    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TweetConfiguration());
    }
}
