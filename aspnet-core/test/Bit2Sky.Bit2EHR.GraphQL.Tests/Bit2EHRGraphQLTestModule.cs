using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Bit2Sky.Bit2EHR.Configure;
using Bit2Sky.Bit2EHR.Startup;
using Bit2Sky.Bit2EHR.Test.Base;

namespace Bit2Sky.Bit2EHR.GraphQL.Tests;

[DependsOn(
    typeof(Bit2EHRGraphQLModule),
    typeof(Bit2EHRTestBaseModule))]
public class Bit2EHRGraphQLTestModule : AbpModule
{
    public override void PreInitialize()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddAndConfigureGraphQL();

        WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(Bit2EHRGraphQLTestModule).GetAssembly());
    }
}