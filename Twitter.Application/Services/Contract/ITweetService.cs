using System.Security.Claims;
using Twitter.Application.Dto.Tweet;
using Twitter.Infrastructure.Entities;

namespace Twitter.Application.Services.Contract;

public interface ITweetService
{
    Task<ReadTweetDto> Get(int id);

    Task<IEnumerable<ReadTweetDto>> GetAll();

    /// <summary>
    /// create tweet
    /// </summary>
    /// <param name="tweetDto"></param>
    /// <returns>created tweet</returns>
    Task<ReadTweetDto> Create(CreateTweetDto tweetDto, ClaimsPrincipal user);

    Task<ReadTweetDto> CreateSubTweet(int mainTweetId, CreateTweetDto tweetDto, ClaimsPrincipal user);

    ReadTweetDto Update(ReadTweetDto tweetDto);

    void Delete(int id);


}
