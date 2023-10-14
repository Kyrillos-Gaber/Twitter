using System.Security.Claims;
using Twitter.Infrastructure.Entities;
using Twitter.Infrastructure.Repository.Contract;
using static System.Net.Mime.MediaTypeNames;

namespace Twitter.Infrastructure.Repository.Implementation;

public class UserRepository : Repository<ApiUser>, IUserRepository
{
    public UserRepository(AppIdentityDbContext dbContext) : base(dbContext) { }

    public async Task<ApiUser> GetUserByUserClaimsPrincipal(ClaimsPrincipal userClaims)
    {
        string userName = userClaims.FindFirst(ClaimTypes.Name)!.Value;
        return await GetAsync(u => u.UserName == userName);
    }
}
