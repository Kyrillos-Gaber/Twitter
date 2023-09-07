using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

    public async Task<T> AddAsyncAndRturnEntity(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public void Delete(Guid id)
    {
        var e = GetById(id);
        Delete(e);
    }

    public void Delete(int id)
    {
        var e = GetById(id);
        Delete(e);
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? expression = null,
        List<string>? includes = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();
        
        if (expression is not null)
            query = query.Where(expression);

        if (includes is not null)
            foreach (string include in includes)
                query.Include(include);

        query.Distinct();

        var res = await query.Skip(0).Take(10).ToListAsync();
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

    public T GetById(int id)
    {
        var res = _dbContext.Set<T>().Find(id);
        return res!;
    }

    public T GetById(Guid id)
    {
        var res = _dbContext.Set<T>().Find(id);
        return res!;
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
}
