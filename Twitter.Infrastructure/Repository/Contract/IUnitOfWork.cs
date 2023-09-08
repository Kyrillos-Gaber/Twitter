namespace Twitter.Infrastructure.Repository.Contract;

public interface IUnitOfWork : IDisposable
{
    ITweetRepository TweetRepository { get; }

    ITagRepository TagRepository { get; }

    Task SaveAsync();

    bool Save();
}