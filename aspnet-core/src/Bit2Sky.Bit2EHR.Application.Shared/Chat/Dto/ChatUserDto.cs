using System;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.Friendships;

namespace Bit2Sky.Bit2EHR.Chat.Dto;

public class ChatUserDto : EntityDto<long>
{
    public int? TenantId { get; set; }

    public Guid? ProfilePictureId { get; set; }

    public string UserName { get; set; }

    public string TenancyName { get; set; }

    public int UnreadMessageCount { get; set; }

    public bool IsOnline { get; set; }

    public FriendshipState State { get; set; }
}

