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
            .HasMany(a => a.Tweets)
            .WithOne(t => t.Author)
            .HasForeignKey(t => t.AuthorId)
            .IsRequired(false);

        modelBuilder.Entity<Tweet>()
            .HasOne(e => e.MainTweet)
            .WithMany(e => e.SubTweets)
            .HasForeignKey(e => e.MainTweetId)
            .IsRequired(false);

        modelBuilder.Entity<Tweet>()
            .HasMany(t => t.Tags)
            .WithMany(t => t.Tweets)
            .UsingEntity<TweetTag>(
                j => j
                    .HasOne(t => t.Tag)
                    .WithMany(t => t.TweetTags)
                    .HasForeignKey(t => t.TagId),
                j => j
                    .HasOne(t => t.Tweet)
                    .WithMany(t => t.TweetTags)
                    .HasForeignKey(t => t.TweetId),
                j => j.HasKey(t => new { t.TweetId, t.TagId }));

    }
}
