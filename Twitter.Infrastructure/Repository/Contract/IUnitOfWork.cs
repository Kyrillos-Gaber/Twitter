namespace Twitter.Infrastructure.Repository.Contract;

public interface IUnitOfWork : IDisposable
{
    ITweetRepository TweetRepository { get; }

    ISubTweetRepository SubTweetRepository { get; }

    ITagRepository TagRepository { get; }

    Task Save();
}