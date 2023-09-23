using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twitter.Application.Dto.User;
using Twitter.Application.Services.Contract;

namespace Twitter.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserManagement _userManagement;

    public UserController(IUserManagement userManagement)
    {
        _userManagement = userManagement;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto createUser)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _userManagement.Register(createUser);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var token = await _userManagement.Login(loginDto);
            if (token is not null) return Ok(new {Token = token});
            return Unauthorized();
        }
        catch(Exception ex)
        {
            return Unauthorized(ex.Message);
        }        
    }
}
