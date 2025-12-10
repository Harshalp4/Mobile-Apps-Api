using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Bit2Sky.Bit2EHR.EntityFrameworkCore;

public static class Bit2EHRDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<Bit2EHRDbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<Bit2EHRDbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}

