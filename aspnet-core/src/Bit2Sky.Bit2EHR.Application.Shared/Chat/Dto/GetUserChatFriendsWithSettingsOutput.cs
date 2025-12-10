using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using Bit2Sky.Bit2EHR.Friendships.Dto;

namespace Bit2Sky.Bit2EHR.Chat.Dto;

public class GetUserChatFriendsWithSettingsOutput
{
    public DateTime ServerTime { get; set; }

    public List<FriendDto> Friends { get; set; }

    public GetUserChatFriendsWithSettingsOutput()
    {
        Friends = new EditableList<FriendDto>();
    }
}

