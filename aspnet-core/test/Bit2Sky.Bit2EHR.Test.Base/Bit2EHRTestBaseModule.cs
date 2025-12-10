using System;
using System.IO;
using Abp;
using Abp.AspNetZeroCore;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.TestBase;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using Bit2Sky.Bit2EHR.Authorization.Users;
using Bit2Sky.Bit2EHR.Configuration;
using Bit2Sky.Bit2EHR.EntityFrameworkCore;
using Bit2Sky.Bit2EHR.MultiTenancy;
using Bit2Sky.Bit2EHR.Security.Recaptcha;
using Bit2Sky.Bit2EHR.Test.Base.DependencyInjection;
using Bit2Sky.Bit2EHR.Test.Base.UiCustomization;
using Bit2Sky.Bit2EHR.Test.Base.Url;
using Bit2Sky.Bit2EHR.Test.Base.Web;
using Bit2Sky.Bit2EHR.UiCustomization;
using Bit2Sky.Bit2EHR.Url;
using NSubstitute;

namespace Bit2Sky.Bit2EHR.Test.Base;

[DependsOn(
    typeof(Bit2EHRApplicationModule),
    typeof(Bit2EHREntityFrameworkCoreModule),
    typeof(AbpTestBaseModule))]
public class Bit2EHRTestBaseModule : AbpModule
{
    public Bit2EHRTestBaseModule(Bit2EHREntityFrameworkCoreModule abpZeroTemplateEntityFrameworkCoreModule)
    {
        abpZeroTemplateEntityFrameworkCoreModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        var configuration = GetConfiguration();

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

        Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(30);
        Configuration.UnitOfWork.IsTransactional = false;

        //Use database for language management
        Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

        RegisterFakeService<AbpZeroDbMigrator>();

        IocManager.Register<IAppUrlService, FakeAppUrlService>();
        IocManager.Register<IWebUrlService, FakeWebUrlService>();
        IocManager.Register<IRecaptchaValidator, FakeRecaptchaValidator>();

        Configuration.ReplaceService<IAppConfigurationAccessor, TestAppConfigurationAccessor>();
        Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
        Configuration.ReplaceService<IUiThemeCustomizerFactory, NullUiThemeCustomizerFactory>();

        Configuration.Modules.AspNetZero().LicenseCode = configuration["AbpZeroLicenseCode"];

        //Uncomment below line to write change logs for the entities below:
        Configuration.EntityHistory.IsEnabled = true;
        Configuration.EntityHistory.Selectors.Add("Bit2EHREntities", typeof(User), typeof(Tenant));
    }

    public override void Initialize()
    {
        ServiceCollectionRegistrar.Register(IocManager);
    }

    private void RegisterFakeService<TService>()
        where TService : class
    {
        IocManager.IocContainer.Register(
            Component.For<TService>()
                .UsingFactoryMethod(() => Substitute.For<TService>())
                .LifestyleSingleton()
        );
    }

    private static IConfigurationRoot GetConfiguration()
    {
        return AppConfigurations.Get(Directory.GetCurrentDirectory(), addUserSecrets: true);
    }
}
