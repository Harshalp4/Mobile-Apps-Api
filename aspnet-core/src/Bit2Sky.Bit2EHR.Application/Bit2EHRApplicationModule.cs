using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Bit2Sky.Bit2EHR.Authorization;

namespace Bit2Sky.Bit2EHR;

/// <summary>
/// Application layer module of the application.
/// </summary>
[DependsOn(
    typeof(Bit2EHRApplicationSharedModule),
    typeof(Bit2EHRCoreModule)
    )]
public class Bit2EHRApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        //Adding authorization providers
        Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

        //Adding custom AutoMapper configuration
        Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(Bit2EHRApplicationModule).GetAssembly());
    }
}
