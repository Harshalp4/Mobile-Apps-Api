using Bit2Sky.Bit2EHR.EntityFrameworkCore;

namespace Bit2Sky.Bit2EHR.Test.Base.TestData;

public class TestDataBuilder
{
    private readonly Bit2EHRDbContext _context;
    private readonly int _tenantId;

    public TestDataBuilder(Bit2EHRDbContext context, int tenantId)
    {
        _context = context;
        _tenantId = tenantId;
    }

    public void Create()
    {
        new TestOrganizationUnitsBuilder(_context, _tenantId).Create();
        new TestSubscriptionPaymentBuilder(_context, _tenantId).Create();
        new TestEditionsBuilder(_context).Create();

        _context.SaveChanges();
    }
}
