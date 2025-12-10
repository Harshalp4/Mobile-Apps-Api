using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.Notifications.Dto;

public class GetNotificationSettingsOutput
{
    public bool ReceiveNotifications { get; set; }

    public List<NotificationSubscriptionWithDisplayNameDto> Notifications { get; set; }
}

