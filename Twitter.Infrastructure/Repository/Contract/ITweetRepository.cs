﻿using Twitter.Infrastructure.Entities;

namespace Twitter.Infrastructure.Repository.Contract;

public interface ITweetRepository : IRepository<Tweet>
{
    Task AddSubTweetAsync(int mainTweetId, Tweet subTweet);
}
