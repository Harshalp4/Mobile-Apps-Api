using Abp;
using Abp.Dependency;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.OpenIddict.EntityFrameworkCore;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Bit2Sky.Bit2EHR.Configuration;
using Bit2Sky.Bit2EHR.EntityHistory;
using Bit2Sky.Bit2EHR.Migrations.Seed;

namespace Bit2Sky.Bit2EHR.EntityFrameworkCore;

[DependsOn(
    typeof(AbpZeroCoreEntityFrameworkCoreModule),
    typeof(Bit2EHRCoreModule),
    typeof(AbpZeroCoreOpenIddictEntityFrameworkCoreModule)
)]
public class Bit2EHREntityFrameworkCoreModule : AbpModule
{
    /* Used it tests to skip DbContext registration, in order to use in-memory database of EF Core */
    public bool SkipDbContextRegistration { get; set; }

    public bool SkipDbSeed { get; set; }

    public override void PreInitialize()
    {
        if (!SkipDbContextRegistration)
        {
            Configuration.Modules.AbpEfCore().AddDbContext<Bit2EHRDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    Bit2EHRDbContextConfigurer.Configure(options.DbContextOptions,
                        options.ExistingConnection);
                }
                else
                {
                    Bit2EHRDbContextConfigurer.Configure(options.DbContextOptions,
                        options.ConnectionString);
                }
            });
        }

        // Set this setting to true for enabling entity history.
        Configuration.EntityHistory.IsEnabled = true;

        // Uncomment below line to write change logs for the entities below:
        Configuration.EntityHistory.Selectors.Add("Bit2EHREntities", EntityHistoryHelper.TrackedTypes);
        Configuration.CustomConfigProviders.Add(new EntityHistoryConfigProvider(Configuration));
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(Bit2EHREntityFrameworkCoreModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        var configurationAccessor = IocManager.Resolve<IAppConfigurationAccessor>();

        using (var scope = IocManager.CreateScope())
        {
            if (!SkipDbSeed && scope.Resolve<DatabaseCheckHelper>()
                    .Exist(configurationAccessor.Configuration["ConnectionStrings:Default"]))
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}

