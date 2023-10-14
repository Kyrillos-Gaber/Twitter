using Microsoft.AspNetCore.SignalR;
using Twitter.Application.Dto.Chat;
using Twitter.Application.Services.Contract.Chat;

namespace Twitter.Application.Services.Implementation;

public sealed class ChatHub : Hub<IChatHub>
{
    public override async Task OnConnectedAsync()
    {
        
    }

    private string GetUserToUserChatGroupName(string from, string to) =>
        string.CompareOrdinal(from, to) < 0 ? $"{from}-{to}" : $"{to}-{from}";

    public async Task CreateUserToUserPrivateChat(MessageDto messageDto)
    {
        string groupName = GetUserToUserChatGroupName(messageDto.From, messageDto.To);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        //Clients.Client().SendAsync.NewPrivateMessage(messageDto);
        
    }
    
}
