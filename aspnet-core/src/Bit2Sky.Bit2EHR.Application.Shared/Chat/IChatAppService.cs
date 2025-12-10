using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.Chat.Dto;

namespace Bit2Sky.Bit2EHR.Chat;

public interface IChatAppService : IApplicationService
{
    Task<GetUserChatFriendsWithSettingsOutput> GetUserChatFriendsWithSettings();

    Task<ListResultDto<ChatMessageDto>> GetUserChatMessages(GetUserChatMessagesInput input);

    Task MarkAllUnreadMessagesOfUserAsRead(MarkAllUnreadMessagesOfUserAsReadInput input);
}

