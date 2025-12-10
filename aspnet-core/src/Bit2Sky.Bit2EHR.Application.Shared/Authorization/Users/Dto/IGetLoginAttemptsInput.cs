using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Dto;

public interface IGetLoginAttemptsInput : ISortedResultRequest
{
    string Filter { get; set; }
}

