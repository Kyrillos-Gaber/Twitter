﻿namespace Twitter.Infrastructure.Repository.Contract;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);

    void Delete(T entity);

    void Delete(Guid id);

    void Delete(int id);

    void Update(T entity);

    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetAsync(int id);
    
    Task<T> GetAsync(Guid id);
}
