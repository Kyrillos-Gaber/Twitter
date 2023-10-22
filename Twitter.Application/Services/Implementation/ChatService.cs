using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Twitter.Application.Services.Implementation;

public class ChatService
{
    public Dictionary<string, List<string>> onlineUsers { get; private set; } = new();

    private string Normalize(string userName) =>
        userName.ToUpper();

    public void AddUser(string userName)
    {
        lock (onlineUsers)
        {
            string name = Normalize(userName);
            foreach (var user in onlineUsers)
                if (onlineUsers.Keys.Contains(name))
                    return;
            onlineUsers.Add(name, null!);
        }
    }

    public void RemoveUser(string userName)
    {
        string user = Normalize(userName);
        lock (onlineUsers)
            if (onlineUsers.ContainsKey(user) && onlineUsers[user].IsNullOrEmpty())
                onlineUsers.Remove(user);
    }

    public void AddUserAndConnectionId(string userName, string connectionId)
    {
        AddUser(userName);
        string user = Normalize(userName);
        lock (onlineUsers)
        {
            if (onlineUsers[user] == null) onlineUsers[user] = new();
            if (!onlineUsers[user].Contains(connectionId))
                onlineUsers[user].Add(connectionId);
        }
    }

    public void RemoveConnectionId(string userName, string connectionId)
    {
        string user = Normalize(userName);
        lock (onlineUsers)
        {
            if (onlineUsers.ContainsKey(user) && onlineUsers[user].Contains(connectionId))
            {
                onlineUsers[user].Remove(connectionId);
                if (onlineUsers[user].Count == 0)
                    onlineUsers.Remove(user);
            }
        }
    }

    public string? GetUserName(string connectionId)
    {
        lock (onlineUsers)
        {
            foreach (var u in onlineUsers)
                if (u.Value.Contains(connectionId))
                    return u.Key;
            return null;
        }
    }

    public List<string>? GetConnectionIdsByUserName(string userName)
    {
        string user = Normalize(userName);
        lock (onlineUsers)
        {
            if (onlineUsers.ContainsKey(user))
                return onlineUsers[user];
            else
                return null;
        }
    }
}
