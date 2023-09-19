using System.ComponentModel.DataAnnotations;
using Twitter.Infrastructure.BusinessModels;
using Twitter.Infrastructure.Entities.Common;

namespace Twitter.Infrastructure.Entities;

public class Tweet : AuditableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(500)]
    public required string Content { get; set; }

    public int? MainTweetId { get; set; }

    public Tweet? MainTweet { get; set; }

    public bool IsMainTweet { get; set; } = false;

    public ICollection<Tweet> SubTweets { get; set; } = new List<Tweet>();

    public Audience Audience { get; set; } = Audience.Public;

    public virtual List<Tag>? Tags { get; set; } 

    public List<TweetTag>? TweetTags { get; set; }

    public /*required*/ string AuthorId { get; set; }

    [Required]
    public /*required*/ ApiUser Author { get; set; }
}
