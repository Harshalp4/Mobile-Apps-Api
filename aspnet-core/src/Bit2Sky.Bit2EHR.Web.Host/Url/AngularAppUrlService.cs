using Abp.MultiTenancy;
using Bit2Sky.Bit2EHR.Url;

namespace Bit2Sky.Bit2EHR.Web.Url;

public class AngularAppUrlService : AppUrlServiceBase
{
    public override string EmailActivationRoute => "account/confirm-email";

    public override string EmailChangeRequestRoute => "account/change-email";

    public override string PasswordResetRoute => "account/reset-password";

    public AngularAppUrlService(
            IWebUrlService webUrlService,
            ITenantCache tenantCache
        ) : base(
            webUrlService,
            tenantCache
        )
    {

    }
}

