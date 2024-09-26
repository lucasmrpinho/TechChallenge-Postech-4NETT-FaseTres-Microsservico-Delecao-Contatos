using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class ConfigurationManagerSetup
    {
        internal static void AddJsonFileByEnvironment(this ConfigurationManager configuration, string environmentName)
        {
            configuration.AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true);
        }
    }
}