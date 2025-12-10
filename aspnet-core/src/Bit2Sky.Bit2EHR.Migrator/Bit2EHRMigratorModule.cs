using Abp.AspNetZeroCore;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using Bit2Sky.Bit2EHR.Configuration;
using Bit2Sky.Bit2EHR.EntityFrameworkCore;
using Bit2Sky.Bit2EHR.Migrator.DependencyInjection;

namespace Bit2Sky.Bit2EHR.Migrator;

[DependsOn(typeof(Bit2EHREntityFrameworkCoreModule))]
public class Bit2EHRMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public Bit2EHRMigratorModule(Bit2EHREntityFrameworkCoreModule abpZeroTemplateEntityFrameworkCoreModule)
    {
        abpZeroTemplateEntityFrameworkCoreModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(Bit2EHRMigratorModule).GetAssembly().GetDirectoryPathOrNull(),
            addUserSecrets: true
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            Bit2EHRConsts.ConnectionStringName
            );
        Configuration.Modules.AspNetZero().LicenseCode = _appConfiguration["AbpZeroLicenseCode"];

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(typeof(IEventBus), () =>
        {
            IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            );
        });
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(Bit2EHRMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}

