using Bit2Sky.Bit2EHR.EntityFrameworkCore;

namespace Bit2Sky.Bit2EHR.Migrations.Seed.Host;

public class InitialHostDbBuilder
{
    private readonly Bit2EHRDbContext _context;

    public InitialHostDbBuilder(Bit2EHRDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        new DefaultEditionCreator(_context).Create();
        new DefaultLanguagesCreator(_context).Create();
        new HostRoleAndUserCreator(_context).Create();
        new DefaultSettingsCreator(_context).Create();

        _context.SaveChanges();
    }
}

