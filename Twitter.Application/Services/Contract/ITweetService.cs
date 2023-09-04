using Twitter.Application.Dto.Tweet;
using Twitter.Infrastructure.Entities;

namespace Twitter.Application.Services.Contract;

public interface ITweetService
{
    Task<ReadTweetDto> Get(Guid id);

    Task<IEnumerable<ReadTweetDto>> GetAll();

    /// <summary>
    /// create tweet
    /// </summary>
    /// <param name="tweetDto"></param>
    /// <returns>created tweet</returns>
    Task<ReadTweetDto> Create(CreateTweetDto tweetDto);
    
    ReadTweetDto Update(ReadTweetDto tweetDto);

    void Delete(Guid id);


}
