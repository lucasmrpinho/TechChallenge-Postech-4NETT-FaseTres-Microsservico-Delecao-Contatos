using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactDelete.IntegrationTests.Configurations.Factories;

namespace Postech.GroupEight.TechChallenge.ContactDelete.IntegrationTests.Fixtures
{
    [ExcludeFromCodeCoverage]
    public class IntegrationTestFixture : IAsyncLifetime
    {
        public ContactDeleteAppWebApplicationFactory WebApplicationFactory { get; private set; }
        public static string RabbitMqConnectionString => TestContainerFactory.RabbitMqConnectionString;

        public async Task DisposeAsync()
        {
            await TestContainerFactory.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            await TestContainerFactory.EnsureInitialized();
            WebApplicationFactory = new(RabbitMqConnectionString);
        }
    }
}