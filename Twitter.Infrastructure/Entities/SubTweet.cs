using System.ComponentModel.DataAnnotations;
using Twitter.Infrastructure.Entities.Common;

namespace Twitter.Infrastructure.Entities;

public class SubTweet : AuditableEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(500)]
    public required string Content { get; set; }

    [Required]
    public Guid MainTweetId { get; set; }

    [Required]
    public required Tweet MainTweet { get; set; }
}