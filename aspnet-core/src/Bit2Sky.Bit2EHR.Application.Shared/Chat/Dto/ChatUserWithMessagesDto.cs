using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.Chat.Dto;

public class ChatUserWithMessagesDto : ChatUserDto
{
    public List<ChatMessageDto> Messages { get; set; }
}

