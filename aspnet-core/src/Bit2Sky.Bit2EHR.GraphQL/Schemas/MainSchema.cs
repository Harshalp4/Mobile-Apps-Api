using Abp.Dependency;
using GraphQL.Types;
using Bit2Sky.Bit2EHR.Queries.Container;
using System;
using GraphQL.Conversion;
using Microsoft.Extensions.DependencyInjection;

namespace Bit2Sky.Bit2EHR.Schemas;

public class MainSchema : Schema, ITransientDependency
{
    public MainSchema(IServiceProvider provider) :
        base(provider)
    {
        Query = provider.GetRequiredService<QueryContainer>();
        NameConverter = new CamelCaseNameConverter();
    }
}

