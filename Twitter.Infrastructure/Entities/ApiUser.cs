using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Twitter.Infrastructure.Entities;

public class ApiUser : IdentityUser
{
    [Required, MinLength(1), MaxLength(50)]
    public required string FirstName { get; set; }

    [Required, MinLength(1), MaxLength(50)]
    public required string LastName { get; set; }

    [Required, MinLength(1), MaxLength(100)]
    public required string Country { get; set; }

    public ICollection<Tweet>? Tweets { get; set; }

}
