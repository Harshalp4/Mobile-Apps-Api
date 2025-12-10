using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Bit2Sky.Bit2EHR.Web.Authentication.JwtBearer;

public class AsyncJwtBearerOptions : JwtBearerOptions
{
    public readonly List<IAsyncSecurityTokenValidator> AsyncSecurityTokenValidators;

    private readonly Bit2EHRAsyncJwtSecurityTokenHandler _defaultAsyncHandler = new Bit2EHRAsyncJwtSecurityTokenHandler();

    public AsyncJwtBearerOptions()
    {
        AsyncSecurityTokenValidators = new List<IAsyncSecurityTokenValidator>() { _defaultAsyncHandler };
    }
}


