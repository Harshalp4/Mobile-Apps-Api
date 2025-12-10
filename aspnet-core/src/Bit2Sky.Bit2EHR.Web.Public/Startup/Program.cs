using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Bit2Sky.Bit2EHR.Web.Public.Startup;

public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        return new WebHostBuilder()
            .UseKestrel(opt => opt.AddServerHeader = false)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIIS()
            .UseIISIntegration()
            .UseStartup<Startup>();
    }
}

