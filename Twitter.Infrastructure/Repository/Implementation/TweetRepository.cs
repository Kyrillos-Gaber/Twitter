using Twitter.Infrastructure.Entities;
using Twitter.Infrastructure.Repository.Contract;

namespace Twitter.Infrastructure.Repository.Implementation;

public class TweetRepository : Repository<Tweet>, ITweetRepository
{
    public TweetRepository(AppIdentityDbContext dbContext) : base(dbContext) { }

}
