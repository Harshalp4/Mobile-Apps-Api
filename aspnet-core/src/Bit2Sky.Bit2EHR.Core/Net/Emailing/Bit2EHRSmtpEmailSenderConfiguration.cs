using Abp.Configuration;
using Abp.Net.Mail;
using Abp.Net.Mail.Smtp;
using Abp.Runtime.Security;

namespace Bit2Sky.Bit2EHR.Net.Emailing;

public class Bit2EHRSmtpEmailSenderConfiguration : SmtpEmailSenderConfiguration
{
    public Bit2EHRSmtpEmailSenderConfiguration(ISettingManager settingManager) : base(settingManager)
    {

    }

    public override string Password => SimpleStringCipher.Instance.Decrypt(GetNotEmptySettingValue(EmailSettingNames.Smtp.Password));
}

