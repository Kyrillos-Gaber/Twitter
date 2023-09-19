using System.ComponentModel.DataAnnotations;

namespace Twitter.Application.Dto.User;

public class CreateUserDto
{
    [Required]
    public required string FirstName { get; set; }

    [Required] 
    public required string LastName { get;set; }

    [Required, DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required, DataType(DataType.Password), Compare("Password")]
    public required string ConfirmPassword { get; set; }
}
