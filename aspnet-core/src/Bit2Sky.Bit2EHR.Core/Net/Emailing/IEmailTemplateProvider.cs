namespace Bit2Sky.Bit2EHR.Net.Emailing;

public interface IEmailTemplateProvider
{
    string GetDefaultTemplate(int? tenantId);
}

