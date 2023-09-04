namespace Twitter.Infrastructure.Entities.Common;

public class AuditableEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? LastUpdateAt { get; set; } = DateTime.Now;
}
