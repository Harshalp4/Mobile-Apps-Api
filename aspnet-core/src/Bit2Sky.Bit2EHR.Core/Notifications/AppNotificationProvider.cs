using Abp.Authorization;
using Abp.Localization;
using Abp.Notifications;
using Bit2Sky.Bit2EHR.Authorization;

namespace Bit2Sky.Bit2EHR.Notifications;

public class AppNotificationProvider : NotificationProvider
{
    public override void SetNotifications(INotificationDefinitionContext context)
    {
        context.Manager.Add(
            new NotificationDefinition(
                AppNotificationNames.NewUserRegistered,
                displayName: L("NewUserRegisteredNotificationDefinition"),
                permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Users)
                )
            );

        context.Manager.Add(
            new NotificationDefinition(
                AppNotificationNames.NewTenantRegistered,
                displayName: L("NewTenantRegisteredNotificationDefinition"),
                permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenants)
                )
            );
    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, Bit2EHRConsts.LocalizationSourceName);
    }
}

