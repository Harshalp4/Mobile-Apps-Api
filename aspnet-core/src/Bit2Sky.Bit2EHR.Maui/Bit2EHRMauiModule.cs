using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Bit2Sky.Bit2EHR.ApiClient;
using Bit2Sky.Bit2EHR.Maui.Core;

namespace Bit2Sky.Bit2EHR.Maui;

[DependsOn(typeof(Bit2EHRClientModule), typeof(AbpAutoMapperModule))]
public class Bit2EHRMauiModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Localization.IsEnabled = false;
        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

        Configuration.ReplaceService<IApplicationContext, MauiApplicationContext>();
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(Bit2EHRMauiModule).GetAssembly());
    }
}