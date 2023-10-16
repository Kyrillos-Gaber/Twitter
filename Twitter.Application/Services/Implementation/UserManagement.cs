using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Twitter.Application.Dto.User;
using Twitter.Application.Services.Contract;
using Twitter.Infrastructure.Entities;
using Twitter.Infrastructure.Repository.Contract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;

namespace Twitter.Application.Services.Implementation;

public class UserManagement : IUserManagement
{
    private readonly UserManager<ApiUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private ApiUser? _user;

    public UserManagement(UserManager<ApiUser> userManager, IConfiguration configuration, IMapper mapper, IFileService fileService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        if (await ValidateUser(loginDto))
            return await CreateToken();
        throw new Exception("Not Authorized!");
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Environment.GetEnvironmentVariable("KEY");
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task Register(CreateUserDto createUserDto)
    {
        createUserDto.Roles!.Clear();
        createUserDto.Roles!.Add("User");
        
        ApiUser user = _mapper.Map<ApiUser>(createUserDto);

        user.ProfilePicture = _fileService.SaveImage(createUserDto.ProfilePicture!);

        IdentityResult result = await _userManager.CreateAsync(user, createUserDto.Password);
        
        StringBuilder errors  = new();
        if (!result.Succeeded)
        {
            foreach (var err in result.Errors)
                errors.Append(err.Description);
            throw new Exception(errors.ToString());
        }
    }

    public async Task<string> CreateToken()
    {
        SigningCredentials credentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenetateTokenOptions(credentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private JwtSecurityToken GenetateTokenOptions(SigningCredentials credentials, List<Claim> claims)
    {
        IConfigurationSection jwtSetting = _configuration.GetSection("Jwt");
        DateTime expiration = 
            DateTime.Now.AddDays(Convert.ToDouble(jwtSetting.GetSection("Lifetime").Value));

        var token = new JwtSecurityToken(
            issuer: jwtSetting.GetSection("Issuer").Value,
            claims: claims,
            expires: expiration,
            audience: jwtSetting.GetSection("Audience").Value,
            signingCredentials: credentials );
        return token;
    }

    private async Task<List<Claim>> GetClaims()
    {
        List<Claim> claims = new() 
        {
            new Claim(ClaimTypes.Name, _user?.UserName!)
        };

        var roles = await _userManager.GetRolesAsync(_user!);

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        return claims;
    }

    public async Task<bool> ValidateUser(LoginDto userLoginDto)
    {
        _user = await _userManager.FindByNameAsync(userLoginDto.Name);

        return (_user is not null 
            && await _userManager.CheckPasswordAsync(_user, userLoginDto.Password));
    }
}
