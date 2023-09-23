using System.ComponentModel.DataAnnotations;

namespace Twitter.Application.Dto.User;

public class LoginDto
{
    [Required]
    public required string Name { get; set; }

    [Required, DataType(DataType.Password)]
    public required string Password { get; set; }
}
