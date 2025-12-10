using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Bit2Sky.Bit2EHR;

[DependsOn(typeof(Bit2EHRCoreSharedModule))]
public class Bit2EHRApplicationSharedModule : AbpModule
{
    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(Bit2EHRApplicationSharedModule).GetAssembly());
    }
}

