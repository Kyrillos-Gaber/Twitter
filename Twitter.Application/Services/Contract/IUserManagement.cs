using Twitter.Application.Dto.User;

namespace Twitter.Application.Services.Contract;

public interface IUserManagement
{
    Task Register(CreateUserDto createUserDto);

    Task<string> Login(LoginDto loginDto);
}
