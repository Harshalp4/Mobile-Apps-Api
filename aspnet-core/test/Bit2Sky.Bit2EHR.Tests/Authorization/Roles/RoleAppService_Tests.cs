using System.Threading.Tasks;
using Bit2Sky.Bit2EHR.Authorization.Roles;
using Bit2Sky.Bit2EHR.Authorization.Roles.Dto;
using Bit2Sky.Bit2EHR.Test.Base;
using Shouldly;
using Xunit;

namespace Bit2Sky.Bit2EHR.Tests.Authorization.Roles;

// ReSharper disable once InconsistentNaming
public class RoleAppService_Tests : AppTestBase
{
    private readonly IRoleAppService _roleAppService;

    public RoleAppService_Tests()
    {
        _roleAppService = Resolve<IRoleAppService>();
    }

    [MultiTenantFact]
    public async Task Should_Get_Roles_For_Host()
    {
        LoginAsHostAdmin();

        //Act
        var output = await _roleAppService.GetRoles(new GetRolesInput());

        //Assert
        output.Items.Count.ShouldBe(1);
    }

    [Fact]
    public async Task Should_Get_Roles_For_Tenant()
    {
        //Act
        var output = await _roleAppService.GetRoles(new GetRolesInput());

        //Assert
        output.Items.Count.ShouldBe(2);
    }
}
