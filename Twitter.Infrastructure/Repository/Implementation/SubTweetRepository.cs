using Twitter.Infrastructure.Entities;
using Twitter.Infrastructure.Repository.Contract;

namespace Twitter.Infrastructure.Repository.Implementation;

public class SubTweetRepository : Repository<SubTweet>, ISubTweetRepository
{
    public SubTweetRepository(AppIdentityDbContext dbContext) :base(dbContext){ }
}