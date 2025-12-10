using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Bit2Sky.Bit2EHR.Localization;

public static class Bit2EHRLocalizationConfigurer
{
    public static void Configure(ILocalizationConfiguration localizationConfiguration)
    {
        localizationConfiguration.Sources.Add(
            new DictionaryBasedLocalizationSource(
                Bit2EHRConsts.LocalizationSourceName,
                new XmlEmbeddedFileLocalizationDictionaryProvider(
                    typeof(Bit2EHRLocalizationConfigurer).GetAssembly(),
                    "Bit2Sky.Bit2EHR.Localization.Bit2EHR"
                )
            )
        );
    }
}

