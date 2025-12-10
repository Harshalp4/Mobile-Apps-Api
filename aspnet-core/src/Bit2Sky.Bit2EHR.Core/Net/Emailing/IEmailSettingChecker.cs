using System.Threading.Tasks;

namespace Bit2Sky.Bit2EHR.Net.Emailing;

public interface IEmailSettingsChecker
{
    bool EmailSettingsValid();

    Task<bool> EmailSettingsValidAsync();
}

