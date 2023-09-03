using System.ComponentModel.DataAnnotations;
using Twitter.Infrastructure.BusinessModels;
using Twitter.Infrastructure.Entities.Common;

namespace Twitter.Infrastructure.Entities;

public class Tweet : AuditableEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(500)]
    public string Content { get; set; } = string.Empty;

    public ICollection<SubTweet>? SubTweets { get; set; }

    public Audience Audience { get; set; } = Audience.None;

    public List<Tag> Tags { get; set; } = new ();
}
