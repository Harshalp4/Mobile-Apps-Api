using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Bit2Sky.Bit2EHR.Configuration;
using Bit2Sky.Bit2EHR.Web;

namespace Bit2Sky.Bit2EHR.EntityFrameworkCore;

/* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
public class Bit2EHRDbContextFactory : IDesignTimeDbContextFactory<Bit2EHRDbContext>
{
    public Bit2EHRDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<Bit2EHRDbContext>();

        /*
         You can provide an environmentName parameter to the AppConfigurations.Get method. 
         In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
         Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
         https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
         */
        var configuration = AppConfigurations.Get(
            WebContentDirectoryFinder.CalculateContentRootFolder(),
            addUserSecrets: true
        );

        Bit2EHRDbContextConfigurer.Configure(builder, configuration.GetConnectionString(Bit2EHRConsts.ConnectionStringName));

        return new Bit2EHRDbContext(builder.Options);
    }
}

