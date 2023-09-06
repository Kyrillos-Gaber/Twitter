using System.ComponentModel.DataAnnotations;
using Twitter.Application.Dto.Common;
using Twitter.Infrastructure.BusinessModels;
using Twitter.Infrastructure.Entities;

namespace Twitter.Application.Dto.Tweet;

public class ReadTweetDto : AuditableEntity
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(500)]
    public required string Content { get; set; }

    public ICollection<ReadTweetDto>? SubTweets { get; set; } 

    public Audience Audience { get; set; } = Audience.Public;

    //public List<Tag>? Tags { get; set; }
}
