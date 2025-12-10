namespace Bit2Sky.Bit2EHR.Maui.Services.Tenants;

public interface ITenantCustomizationService
{
    Task<string> GetTenantLogo();
}