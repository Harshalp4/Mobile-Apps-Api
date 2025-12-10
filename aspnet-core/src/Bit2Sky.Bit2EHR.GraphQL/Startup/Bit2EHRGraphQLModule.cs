using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Bit2Sky.Bit2EHR.Startup;

[DependsOn(typeof(Bit2EHRCoreModule))]
public class Bit2EHRGraphQLModule : AbpModule
{
    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(Bit2EHRGraphQLModule).GetAssembly());
    }

    public override void PreInitialize()
    {
        base.PreInitialize();

        //Adding custom AutoMapper configuration
        Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
    }
}

