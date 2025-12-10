using Microsoft.Data.SqlClient;
using Shouldly;
using Xunit;

namespace Bit2Sky.Bit2EHR.Tests.General;

// ReSharper disable once InconsistentNaming
public class ConnectionString_Tests
{
    [Fact]
    public void SqlConnectionStringBuilder_Test()
    {
        var csb = new SqlConnectionStringBuilder("Server=localhost; Database=Bit2EHR; Trusted_Connection=True;");
        csb["Database"].ShouldBe("Bit2EHR");
    }
}
