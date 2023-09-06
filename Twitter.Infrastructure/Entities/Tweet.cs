﻿using System.ComponentModel.DataAnnotations;
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

    //public ICollection<SubTweet>? SubTweets { get; set; }

    public Guid? MainTweetId { get; set; }

    public Tweet? MainTweet { get; set; }

    public bool IsMainTweet { get; set; } = false;

    public ICollection<Tweet> SubTweets { get; set; } = new List<Tweet>();

    public Audience Audience { get; set; } = Audience.Public;

    public List<Tag>? Tags { get; set; }
}
