using System.ComponentModel.DataAnnotations;
using Twitter.Infrastructure.BusinessModels;

namespace Twitter.Application.Dto.Tweet;

public class CreateTweetDto
{
    [Required]
    [MaxLength(500)]
    public required string Content { get; set; }

    public Audience Audience { get; set; } = Audience.Public;

}
