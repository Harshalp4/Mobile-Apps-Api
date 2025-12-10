using Abp.Notifications;

namespace Bit2Sky.Bit2EHR.Notifications;

public class SendNotificationToAllUsersArgs
{
    public string NotificationName { get; set; }
    public string Message { get; set; }
    public NotificationSeverity Severity { get; set; }
}