using Twitter.Application.Services.Implementation;

namespace Twitter.Application.Tests.ServicesTests;

public class ChatServiceTests
{
    private ChatService _chatService = new ChatService();
   
    //[Fact]
    //public void Method_Scenario_Outcome()

    [Fact]
    public void AddUser()
    {
        _chatService.AddUser("kyro");
        Assert.Contains("KYRO", _chatService.onlineUsers.Keys);
    }

    [Fact]
    public void RemoveUser()
    {
        _chatService.AddUser("kyro");
        _chatService.RemoveUser("kyro");
        Assert.DoesNotContain("KYRO", _chatService.onlineUsers.Keys);
    }

    [Fact]
    public void RemoveUser_IfUserHasSingleConnectionId()
    {
        _chatService.AddUserAndConnectionId("kyro", Guid.NewGuid().ToString());
        _chatService.RemoveUser("kyro");
        Assert.Contains("KYRO", _chatService.onlineUsers.Keys);
    }

    [Fact]
    public void RemoveConnectionId_IfSingleId()
    {
        string id = Guid.NewGuid().ToString();
        _chatService.AddUserAndConnectionId("Sameh", id);
        _chatService.RemoveConnectionId("Sameh", id);
        Assert.DoesNotContain("SAMEH", _chatService.onlineUsers.Keys);
    }

    [Fact]
    public void RemoveConnectionId_IfManyIds()
    {
        string id = Guid.NewGuid().ToString();
        _chatService.AddUserAndConnectionId("Sameh", id);
        _chatService.AddUserAndConnectionId("Sameh", Guid.NewGuid().ToString());
        _chatService.RemoveConnectionId("Sameh", id);
        Assert.Contains("SAMEH", _chatService.onlineUsers.Keys);
    }

    [Fact]
    public void AddUserAndConnectionId()
    {
        string id = Guid.NewGuid().ToString();
        string name = "Sameh";
        _chatService.AddUserAndConnectionId(name, id);
        Assert.Contains(name.ToUpper(), _chatService.onlineUsers.Keys);
        Assert.Contains(id, _chatService.onlineUsers[name.ToUpper()]);
    }

    [Fact]
    public void GetUserName()
    {
        string id = Guid.NewGuid().ToString();
        string id2 = Guid.NewGuid().ToString();
        string name = "SASA";
        _chatService.AddUserAndConnectionId(name, id);
        _chatService.AddUserAndConnectionId(name, id2);
        Assert.Equal(name, _chatService.GetUserName(id)!);
        Assert.Equal(name, _chatService.GetUserName(id2));
    }

    [Fact]
    public void GetConnectionIdsByUserName()
    {
        string id = Guid.NewGuid().ToString();

        List<string> ids = new()
        {
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString()
        };
        string name = "YAYA";
        _chatService.AddUserAndConnectionId(name, ids[0]);
        _chatService.AddUserAndConnectionId(name, ids[1]);
        Assert.Equal(ids, _chatService.GetConnectionIdsByUserName(name));
    }
}
