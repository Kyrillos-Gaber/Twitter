using Microsoft.EntityFrameworkCore;
using Twitter.Infrastructure.Entities;

namespace Twitter.Infrastructure.Repository.Contract;

public interface ITagRepository : IRepository<Tag>
{
    public Task AddRangeAsynce(List<Tag> tags);
    
    public Task<List<Tag>> AddRangeAsynce(List<string> tags);

    public Tag AddIfNotExists(Tag tag);


}
