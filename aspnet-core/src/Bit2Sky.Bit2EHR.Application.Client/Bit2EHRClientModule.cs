using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Bit2Sky.Bit2EHR;

public class Bit2EHRClientModule : AbpModule
{
    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(Bit2EHRClientModule).GetAssembly());
    }
}

