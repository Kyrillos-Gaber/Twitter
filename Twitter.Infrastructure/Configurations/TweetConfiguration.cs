using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Twitter.Infrastructure.Entities;

namespace Twitter.Infrastructure.Configurations;

public class TweetConfiguration : IEntityTypeConfiguration<Tweet>
{
    public void Configure(EntityTypeBuilder<Tweet> builder)
    {
        builder
            .HasOne(e => e.MainTweet)
            .WithMany(e => e.SubTweets)
            .HasForeignKey(e => e.MainTweetId)
            .IsRequired(false);

        builder
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
