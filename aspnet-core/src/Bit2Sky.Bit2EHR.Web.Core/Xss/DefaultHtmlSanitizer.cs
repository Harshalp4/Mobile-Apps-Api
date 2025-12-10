using System.Text.RegularExpressions;

namespace Bit2Sky.Bit2EHR.Web.Xss;


public class DefaultHtmlSanitizer : IHtmlSanitizer
{
    public string Sanitize(string html)
    {
        return Regex.Replace(html, "<.*?>|&.*?;", string.Empty);
    }
}

