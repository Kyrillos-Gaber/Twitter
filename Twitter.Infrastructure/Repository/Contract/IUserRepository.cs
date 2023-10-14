using System.Security.Claims;
using Twitter.Infrastructure.Entities;

namespace Twitter.Infrastructure.Repository.Contract;

public interface IUserRepository : IRepository<ApiUser>
{
    Task<ApiUser> GetUserByUserClaimsPrincipal(ClaimsPrincipal userClaims);
}
