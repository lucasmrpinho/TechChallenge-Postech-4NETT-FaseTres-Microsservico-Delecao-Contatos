using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Postech.GroupEight.TechChallenge.ContactDelete.IntegrationTests.Configurations.Factories;
using Postech.GroupEight.TechChallenge.ContactDelete.IntegrationTests.Fixtures;

namespace Postech.GroupEight.TechChallenge.ContactDelete.IntegrationTests.Configurations.Base
{
    [ExcludeFromCodeCoverage]
    public class IntegrationTestBase : IClassFixture<IntegrationTestFixture>
    {
        protected readonly HttpClient HttpClient;
        protected readonly ContactDeleteAppWebApplicationFactory WebApplicationFactory;

        protected IntegrationTestBase(IntegrationTestFixture fixture)
        {
            WebApplicationFactory = fixture.WebApplicationFactory;
            HttpClient = WebApplicationFactory.CreateClient();
        }

        protected T GetService<T>() 
            where T : notnull
        {
            IServiceScope scope = WebApplicationFactory.Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<T>();
        }
    }
}