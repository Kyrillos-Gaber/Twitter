using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        modelBuilder.Entity<ApiUser>()
            .HasMany(t => t.Tweets)
            .WithOne()
            .IsRequired(false);

        modelBuilder.Entity<Tweet>()
            .HasMany(t => t.Tags)
            .WithMany(t => t.Tweets);

        /*
        modelBuilder.Entity<Tweet>()
            .HasMany(t => t.SubTweets)
            .WithMany()
            .UsingEntity<SubTweet>(
                e => e.HasOne<Tweet>().WithMany().HasForeignKey(e => e.TweetId),
                e => e.HasOne<Tweet>().WithMany().HasForeignKey(e => e.SubTweetId));
        */

        modelBuilder.Entity<Tweet>()
            .HasOne(e => e.MainTweet)
            .WithMany(e => e.SubTweets)
            .HasForeignKey(e => e.MainTweetId)
            .IsRequired(false);

        /*
                modelBuilder.Entity<Tag>()
                    .HasIndex(e => e.Name)
                    .IsUnique(true);
        */
    }
}
