using Twitter.Application.Dto.Chat;

namespace Twitter.Application.Services.Contract.Chat;

public interface IChatHub
{
    Task NewPrivateMessage(MessageDto messageDto);
}
