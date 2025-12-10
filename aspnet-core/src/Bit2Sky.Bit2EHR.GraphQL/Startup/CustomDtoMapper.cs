using AutoMapper;
using Bit2Sky.Bit2EHR.Authorization.Users;
using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.Startup;

public static class CustomDtoMapper
{
    public static void CreateMappings(IMapperConfigurationExpression configuration)
    {
        configuration.CreateMap<User, UserDto>()
            .ForMember(dto => dto.Roles, options => options.Ignore())
            .ForMember(dto => dto.OrganizationUnits, options => options.Ignore());
    }
}

