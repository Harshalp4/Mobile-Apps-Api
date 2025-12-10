using Abp.Dependency;
using Castle.MicroKernel.Registration;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bit2Sky.Bit2EHR.EntityFrameworkCore;
using Bit2Sky.Bit2EHR.Identity;

namespace Bit2Sky.Bit2EHR.Test.Base.DependencyInjection;

public static class ServiceCollectionRegistrar
{
    public static void Register(IIocManager iocManager)
    {
        RegisterIdentity(iocManager);

        var builder = new DbContextOptionsBuilder<Bit2EHRDbContext>();

        var inMemorySqlite = new SqliteConnection("Data Source=:memory:");
        builder.UseSqlite(inMemorySqlite);

        iocManager.IocContainer.Register(
            Component
                .For<DbContextOptions<Bit2EHRDbContext>>()
                .Instance(builder.Options)
                .LifestyleSingleton()
        );

        inMemorySqlite.Open();

        new Bit2EHRDbContext(builder.Options).Database.EnsureCreated();
    }

    private static void RegisterIdentity(IIocManager iocManager)
    {
        var services = new ServiceCollection();

        IdentityRegistrar.Register(services);

        WindsorRegistrationHelper.CreateServiceProvider(iocManager.IocContainer, services);
    }
}
