namespace Twitter.Infrastructure.Repository.Contract;

public interface IUnitOfWork : IDisposable
{
    ITweetRepository TweetRepository { get; }
    
    IUserRepository UserRepository { get; }

    ITagRepository TagRepository { get; }

    Task SaveAsync();

    bool Save();
}