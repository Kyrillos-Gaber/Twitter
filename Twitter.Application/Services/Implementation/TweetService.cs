using AutoMapper;
using Twitter.Application.Dto.Tweet;
using Twitter.Application.Services.Contract;
using Twitter.Infrastructure.Entities;
using Twitter.Infrastructure.Repository.Contract;

namespace Twitter.Application.Services.Implementation;

public class TweetService : ITweetService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TweetService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ReadTweetDto> Create(CreateTweetDto tweetDto)
    {
        // Destination => Source
        var tweet = _mapper.Map<Tweet>(tweetDto);
        tweet.IsMainTweet = true;

        tweet.Tags = await _unitOfWork.TagRepository.AddRangeAsynce(tweetDto.Tags!);

        await _unitOfWork.TweetRepository.AddAsync(tweet);
        await _unitOfWork.SaveAsync();
        
        return _mapper.Map<ReadTweetDto>(tweet);
    }

    public async Task<ReadTweetDto> CreateSubTweet(int mainTweetId, CreateTweetDto tweetDto)
    {
        Tweet entity = _mapper.Map<Tweet>(tweetDto);
        entity.IsMainTweet = false;
        await _unitOfWork.TweetRepository.AddSubTweetAsync(mainTweetId, entity);
        await _unitOfWork.SaveAsync();

        Tweet tweet = await _unitOfWork.TweetRepository.GetAsync(mainTweetId);
        return _mapper.Map<ReadTweetDto>(tweet);
    }

    public void Delete(int id)
    {
        _unitOfWork.TweetRepository.Delete(id);
        if (!_unitOfWork.Save())
            throw new Exception("can not delete!");
    }

    public async Task<ReadTweetDto> Get(int id)
    {
        var tweet = await _unitOfWork.TweetRepository.GetAsync(id);
        return _mapper.Map<ReadTweetDto>(tweet);
    }

    public async Task<IEnumerable<ReadTweetDto>> GetAll()
    {
        var tweets = await _unitOfWork.TweetRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ReadTweetDto>>(tweets);
    }

    public ReadTweetDto Update(ReadTweetDto tweetDto)
    {
        var tweet = _mapper.Map<Tweet>(tweetDto);
        tweet.LastUpdateAt = DateTime.Now;
        _unitOfWork.TweetRepository.Update(tweet);
        _unitOfWork.Save();
        return _mapper.Map<ReadTweetDto>(tweet);
    }
}
