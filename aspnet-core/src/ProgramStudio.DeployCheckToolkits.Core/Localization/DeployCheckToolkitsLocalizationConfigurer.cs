using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace ProgramStudio.DeployCheckToolkits.Localization
{
    public static class DeployCheckToolkitsLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(DeployCheckToolkitsConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(DeployCheckToolkitsLocalizationConfigurer).GetAssembly(),
                        "ProgramStudio.DeployCheckToolkits.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
