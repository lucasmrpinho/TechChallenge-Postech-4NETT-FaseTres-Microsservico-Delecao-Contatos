using System.Net;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Bogus;
using FluentAssertions;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Deserializers.Extensions;
using Postech.GroupEight.TechChallenge.ContactDelete.IntegrationTests.Configurations.Base;
using Postech.GroupEight.TechChallenge.ContactDelete.IntegrationTests.Fixtures;

namespace Postech.GroupEight.TechChallenge.ContactDelete.IntegrationTests.Suite.Infra.Controllers
{
    [Collection("Integration Tests")]
    public class ContactsControllerTests(IntegrationTestFixture fixture) : IntegrationTestBase(fixture)
    {
        private readonly Faker _faker = new("pt_BR");

        [Fact(DisplayName = "Request to delete a contact at the /contacts/{contactId} endpoint")]
        [Trait("Action", "/contacts/{contactId}")]
        public async Task DeleteContactEndpoint_RequestAnDeletionOfContact_ShouldReturn202Accepted()
        {
            // Arrange
            Guid contactId = Guid.NewGuid();
            DeleteContactRequestCommand requestCommand = new()
            {
                ContactId = contactId,
            };

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"/contacts/{contactId}")
            {
                Content = new StringContent(JsonSerializer.Serialize(requestCommand), Encoding.UTF8, "application/json"),
            });

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Accepted);
            GenericResponseCommand<DeleteContactResponseCommand>? responseMessageContent = await responseMessage.Content.AsAsync<GenericResponseCommand<DeleteContactResponseCommand>>();
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Data.Should().NotBeNull();
            responseMessageContent?.IsSuccessfullyProcessed.Should().BeTrue();
            responseMessageContent?.Data?.ContactId.Should().Be(contactId);
            responseMessageContent?.Data?.ContactNotifiedForDeleteAt.Should().BeOnOrBefore(DateTime.UtcNow);
            responseMessageContent?.Data?.IsContactNotifiedForDelete.Should().BeTrue();
            responseMessageContent?.Messages.Should().BeNull();
        }

        [Fact(DisplayName = "Request body not provided for /contacts/{contactId} endpoint")]
        [Trait("Action", "/contacts/{contactId}")]
        public async Task DeleteContactEndpoint_RequestBodyNotProvided_ShouldReturn400BadRequest()
        {
            // Arrange
            Guid contactId = Guid.NewGuid();

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"/contacts/{contactId}"));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            GenericResponseCommand<DeleteContactResponseCommand>? responseMessageContent = await responseMessage.Content.AsAsync<GenericResponseCommand<DeleteContactResponseCommand>>();
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Messages.Should().NotBeNullOrEmpty();
        }

        [Fact(DisplayName = "Content-Type not supported for /contacts/{contactId} endpoint")]
        [Trait("Action", "/contacts/{contactId}")]
        public async Task DeleteContactEndpoint_ContentTypeNotSupported_ShouldReturn415UnsupportedMediaType()
        {
            // Arrange
            Guid contactId = Guid.NewGuid();
            DeleteContactRequestCommand requestCommand = new()
            {
                ContactId = contactId,
            };
            XmlSerializer serializer = new(typeof(DeleteContactRequestCommand));
            using StringWriter stringWriter = new();
            serializer.Serialize(stringWriter, requestCommand);

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"/contacts/{contactId}")
            {
                Content = new StringContent(stringWriter.ToString(), Encoding.UTF8, "application/xml"),
            });

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
        }


        [Fact(DisplayName = "Contact identifier differs between request body and path data at the /contacts/{contactId} endpoint")]
        [Trait("Action", "/contacts/{contactId}")]
        public async Task DeleteContactEndpoint_ContactIdentifierDiffersBetweenRequestBodyAndPathData_ShouldReturn400BadRequest()
        {
            // Arrange
            Guid bodyContactId = Guid.NewGuid();
            Guid pathContactId = Guid.NewGuid();
            DeleteContactRequestCommand requestCommand = new()
            {
                ContactId = bodyContactId,
            };

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"/contacts/{pathContactId}")
            {
                Content = new StringContent(JsonSerializer.Serialize(requestCommand), Encoding.UTF8, "application/json"),
            });

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            GenericResponseCommand<DeleteContactResponseCommand>? responseMessageContent = await responseMessage.Content.AsAsync<GenericResponseCommand<DeleteContactResponseCommand>>();
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Messages.Should().NotBeNullOrEmpty();
        }
    }  
}