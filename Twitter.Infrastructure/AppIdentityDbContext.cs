using Microsoft.EntityFrameworkCore;
using Twitter.Infrastructure.Entities;

namespace Twitter.Infrastructure;

public class AppIdentityDbContext : DbContext
{
    public AppIdentityDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Tweet> Tweets { get; set; }

    public DbSet<SubTweet> SubTweets { get; set; }

    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tweet>()
            .HasMany(t => t.SubTweets)
            .WithOne(e => e.MainTweet)
            .HasForeignKey(e => e.MainTweetId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Tweet>()
            .HasMany(t => t.Tags)
            .WithMany(t => t.Tweets);
    }
}
