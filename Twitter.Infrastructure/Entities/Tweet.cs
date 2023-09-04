using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Twitter.Infrastructure.BusinessModels;
using Twitter.Infrastructure.Entities.Common;

namespace Twitter.Infrastructure.Entities;

public class Tweet : AuditableEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(500)]
    public required string Content { get; set; }

    public ICollection<SubTweet>? SubTweets { get; set; }

    public Audience Audience { get; set; } = Audience.Public;

    public List<Tag>? Tags { get; set; }
}
