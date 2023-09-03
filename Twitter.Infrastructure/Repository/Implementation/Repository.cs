using Microsoft.EntityFrameworkCore;
using Twitter.Infrastructure.Repository.Contract;

namespace Twitter.Infrastructure.Repository.Implementation;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppIdentityDbContext _dbContext;

    public Repository(AppIdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public async void Delete(Guid id)
    {
        var e = await GetAsync(id);
        Delete(e);
    }

    public async void Delete(int id)
    {
        var e = await GetAsync(id);
        Delete(e);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var res = await _dbContext.Set<T>().Take(1..10).ToListAsync();
        return res;
    }

    public async Task<T> GetAsync(int id)
    {
        var res = await _dbContext.Set<T>().FindAsync(id);
        return res!;
    }

    public async Task<T> GetAsync(Guid id)
    {
        var res = await _dbContext.Set<T>().FindAsync(id);
        return res!;
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
}
