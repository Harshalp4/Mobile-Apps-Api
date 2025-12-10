using System.Threading.Tasks;

namespace Bit2Sky.Bit2EHR.Net.Sms;

public interface ISmsSender
{
    Task SendAsync(string number, string message);
}

