namespace Twitter.Infrastructure.Entities;

public class TweetTag
{
    public int TweetId { get; set; }
    
    public required Tweet Tweet { get; set; }

    public int TagId { get; set; }
    
    public required Tag Tag { get; set; }
}
