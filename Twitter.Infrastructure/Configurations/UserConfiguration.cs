using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Twitter.Infrastructure.Entities;

namespace Twitter.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApiUser>
{
    public void Configure(EntityTypeBuilder<ApiUser> builder)
    {
        builder
            .HasMany(a => a.Tweets)
            .WithOne(t => t.Author)
            .HasForeignKey(t => t.AuthorId)
            .IsRequired(false);
    }
}
