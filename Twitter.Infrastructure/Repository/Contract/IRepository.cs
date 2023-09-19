using System.Linq.Expressions;

namespace Twitter.Infrastructure.Repository.Contract;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);

    Task<T> AddAsyncAndRturnEntity(T entity);

    void Delete(T entity);

    void Delete(int id);

    void Update(T entity);

    Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? expression = null,
        List<string>? includes = null);

    Task<T> GetAsync(int id);
    

    public Task<T> GetAsync(Expression< Func<T, bool> > expression);

    T GetById(int id);

    
    
}
