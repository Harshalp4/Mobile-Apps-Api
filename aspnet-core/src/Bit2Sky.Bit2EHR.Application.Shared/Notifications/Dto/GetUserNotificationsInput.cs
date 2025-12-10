using System;
using Abp.Notifications;
using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.Notifications.Dto;

public class GetUserNotificationsInput : PagedInputDto
{
    public UserNotificationState? State { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}

