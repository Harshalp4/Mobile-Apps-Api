using Abp.Dependency;

namespace Bit2Sky.Bit2EHR;

public class AppFolders : IAppFolders, ISingletonDependency
{
    public string SampleProfileImagesFolder { get; set; }

    public string WebLogsFolder { get; set; }
}

