using System.Threading.Tasks;

namespace Bit2Sky.Bit2EHR.Security;

public interface IPasswordComplexitySettingStore
{
    Task<PasswordComplexitySetting> GetSettingsAsync();
}

