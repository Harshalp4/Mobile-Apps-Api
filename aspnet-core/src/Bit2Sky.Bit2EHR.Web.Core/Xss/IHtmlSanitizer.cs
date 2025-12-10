using Abp.Dependency;

namespace Bit2Sky.Bit2EHR.Web.Xss;

public interface IHtmlSanitizer : ITransientDependency
{
    string Sanitize(string html);
}

