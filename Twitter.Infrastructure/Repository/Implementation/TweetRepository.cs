using Microsoft.EntityFrameworkCore;
using Twitter.Infrastructure.Entities;
using Twitter.Infrastructure.Repository.Contract;

namespace Twitter.Infrastructure.Repository.Implementation;

public class TweetRepository : Repository<Tweet>, ITweetRepository
{
    private readonly AppIdentityDbContext _dbContext;

    public TweetRepository(AppIdentityDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSubTweetAsync(Guid mainTweetId, Tweet subTweet)
    {
        Tweet mainTweet = await GetAsync(mainTweetId);

        subTweet.Audience = mainTweet.Audience;
        subTweet.MainTweetId = mainTweetId;
        
        var entity = await AddAsyncAndRturnEntity(subTweet);
    }


}
