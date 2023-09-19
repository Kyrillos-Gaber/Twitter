using Microsoft.EntityFrameworkCore;
using Twitter.Infrastructure.Entities;
using Twitter.Infrastructure.Repository.Contract;
using static System.Net.Mime.MediaTypeNames;

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

    public async Task<List<Tag>> AddRangeAsynce(List<string> tags)
    {
        List<Tag> tagsList = new ();

        foreach (string tag in tags)
        {
            Tag t = await GetAsync(x => x.Name == tag);
            if (t is null)
            {
                t = new Tag { Name = tag };
                await AddAsync(t);
            }
            t.Count++;
            tagsList.Add(t);
        }

        return tagsList;
    }
}
