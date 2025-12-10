namespace Bit2Sky.Bit2EHR.Configuration;

public interface IAppConfigurationWriter
{
    void Write(string key, string value);
}

