namespace Twitter.Application.Dto.Common;

public class AuditableEntity
{
    public DateTime CreatedAt { get; set; }
    
    public DateTime LastUpdateAt { get; set; }
}
