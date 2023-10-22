using Microsoft.AspNetCore.SignalR;
using Twitter.Application.Dto.Chat;
using Twitter.Application.Services.Contract.Chat;

namespace Twitter.Application.Services.Implementation;

public sealed class ChatHub : Hub<IChatHub>
{
    private readonly ChatService _chatService;

    public ChatHub(ChatService chatService)
    {
        _chatService = chatService;
    }

    public override async Task OnConnectedAsync()
    {
        await Console.Out.WriteLineAsync(Context.ConnectionId);
        await Console.Out.WriteLineAsync();
        
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _chatService.RemoveConnectionId(_chatService.GetUserName(Context.ConnectionId)!, Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    public void Connect(string userName)
    {
        _chatService.AddUserAndConnectionId(userName, Context.ConnectionId);
    }

    private string GetUserToUserChatGroupName(string from, string to) =>
        string.CompareOrdinal(from, to) < 0 ? $"{from}-{to}" : $"{to}-{from}";

    public async Task CreateUserToUserPrivateChat(MessageDto messageDto)
    {
        string groupName = GetUserToUserChatGroupName(messageDto.From, messageDto.To);
        _chatService.AddUserAndConnectionId(messageDto.From, Context.ConnectionId);
        var sendToIds = _chatService.GetConnectionIdsByUserName(messageDto.To);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        
        foreach (string id in sendToIds!)
            await Groups.AddToGroupAsync(id, groupName);
    }

    public async Task ReceivePrivateMessage(MessageDto message)
    {
        await Clients.Group(GetUserToUserChatGroupName(message.From, message.To))
            .NewPrivateMessage(message);
    }

    

}
