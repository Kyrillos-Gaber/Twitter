using Twitter.Infrastructure.Repository.Contract;

namespace Twitter.Infrastructure.Repository.Implementation;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppIdentityDbContext _dbContext;

    public ITweetRepository TweetRepository { get; }

    public ISubTweetRepository SubTweetRepository { get; }

    public ITagRepository TagRepository { get; }

    public UnitOfWork(AppIdentityDbContext dbContext, ITweetRepository tweetRepository, 
        ISubTweetRepository subTweetRepository, ITagRepository tagRepository)
    {
        _dbContext = dbContext;
        TweetRepository = tweetRepository;
        SubTweetRepository = subTweetRepository;
        TagRepository = tagRepository;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public bool Save()
    {
        return _dbContext.SaveChanges() > 0 ;
    }
}
