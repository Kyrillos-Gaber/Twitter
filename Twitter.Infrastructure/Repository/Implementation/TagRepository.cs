using Microsoft.EntityFrameworkCore;
using Twitter.Infrastructure.Entities;
using Twitter.Infrastructure.Repository.Contract;

namespace Twitter.Infrastructure.Repository.Implementation;

public class TagRepository : Repository<Tag>, ITagRepository
{
    private readonly AppIdentityDbContext _dbContext;

    public TagRepository(AppIdentityDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeAsynce(List<Tag> tags)
    {
        try
        {
            foreach (var tag in tags)
                AddIfNotExists(tag);
        }
        catch (Exception ex) { await Console.Out.WriteLineAsync(ex.Message); }
    }

    public Tag AddIfNotExists(Tag tag)
    {
        var dbset = _dbContext.Set<Tag>();
        bool exists = dbset.Any(e => e.Name == tag.Name);

        if (!exists)
            dbset.Add(tag);
                
        return tag;
    }
}
