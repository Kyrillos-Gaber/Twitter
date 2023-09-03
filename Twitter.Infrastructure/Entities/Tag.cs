using System.ComponentModel.DataAnnotations;
using Twitter.Infrastructure.Entities.Common;

namespace Twitter.Infrastructure.Entities;

public class Tag : AuditableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100), MinLength(1)]
    public required string Name { get; set; }
    
    public List<Tweet> Tweets { get; set; } = new ();
}
