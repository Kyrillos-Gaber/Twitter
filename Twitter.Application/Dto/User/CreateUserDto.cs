using System.ComponentModel.DataAnnotations;

namespace Twitter.Application.Dto.User;

public class CreateUserDto
{
    [Required, MinLength(3), MaxLength(50)] 
    public required string UserName { get; set; }

    [Required, MinLength(1), MaxLength(50)]
    public required string FirstName { get; set; }

    [Required, MinLength(1), MaxLength(50)] 
    public required string LastName { get;set; }

    [Required, DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required, DataType(DataType.Password), Compare("Password")]
    public required string ConfirmPassword { get; set; }

    [Required, MinLength(1), MaxLength(100)]
    public required string Country { get; set; }

    public ICollection<string>? Roles { get; set; }

}
