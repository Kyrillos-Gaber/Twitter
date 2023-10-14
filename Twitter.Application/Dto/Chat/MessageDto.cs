using System.ComponentModel.DataAnnotations;

namespace Twitter.Application.Dto.Chat;

public class MessageDto
{
    [Required]
    public required string From { get; set; }

    [Required]
    public required string To { get; set; }

    [Required]
    public required string Content { get; set; }

    public DateTime CreatedAt { get; set; }

}
