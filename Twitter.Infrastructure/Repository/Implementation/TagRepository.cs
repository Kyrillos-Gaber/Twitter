using Twitter.Infrastructure.Entities;
using Twitter.Infrastructure.Repository.Contract;

namespace Twitter.Infrastructure.Repository.Implementation;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(AppIdentityDbContext dbContext) : base(dbContext) { }

}
