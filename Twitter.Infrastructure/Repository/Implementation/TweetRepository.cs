using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using Twitter.Infrastructure.Entities;
using Twitter.Infrastructure.Repository.Contract;
using System.Collections.Generic;

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

    public new async Task<IEnumerable<Tweet>> GetAllAsync(
        Expression<Func<Tweet, bool>>? expression = null,
        List<string>? includes = null)
    {

        var tweetTable = _dbContext.Set<Tweet>();

        var tweets = tweetTable
            .Where(t => t.IsMainTweet == true)
            .Skip(0).Take(10)
            .Include("Tags")
            .ToList();

        foreach (var tweet in tweets)
            tweet.SubTweets = tweetTable
                .Where(t => t.MainTweetId == tweet.Id)
                .Skip(0).Take(5).ToList();

        return tweets;
    }

    
}
