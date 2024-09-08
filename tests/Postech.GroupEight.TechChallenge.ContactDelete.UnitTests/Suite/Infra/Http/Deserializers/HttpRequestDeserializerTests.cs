using System.Text.Json;
using System.Xml.Serialization;
using Bogus;
using FluentAssertions;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Deserializers;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Deserializers.Exceptions;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.UnitTests.Suite.Infra.Http.Deserializers
{
    public class HttpRequestDeserializerTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Fact(DisplayName = "Deserialize a valid request command in application json format")]
        [Trait("Action", "Deserialize")]
        public void Deserialize_DeserializeValidRequestCommandInApplicationJsonFormat_ShouldDeserializeRequestCommand()
        {
            // Arrange
            DeleteContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
            };
            string requestBody = JsonSerializer.Serialize(command);
            string format = "application/json";

            // Act
            DeleteContactRequestCommand? deserializedCommand = HttpRequestDeserializer.Deserialize<DeleteContactRequestCommand>(requestBody, format);

            // Assert
            deserializedCommand.Should().NotBeNull();
            deserializedCommand.Should().Be(command);
        }

        [Fact(DisplayName = "Deserialize a valid request command into an unsupported format")]
        [Trait("Action", "Deserialize")]
        public void Deserialize_DeserializeValidRequestCommandIntoAnUnsupportedFormat_ShouldThrowHttpResponseDeserializerException()
        {
            // Arrange
            DeleteContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
            };
            XmlSerializer xmlSerializer = new(typeof(DeleteContactRequestCommand));
            using StringWriter stringWriter = new();
            xmlSerializer.Serialize(stringWriter, command);
            string requestBody = stringWriter.ToString();
            string unsupportedFormat = "application/xml";

            // Assert
            HttpResponseDeserializerException exception = Assert.Throws<HttpResponseDeserializerException>(() => HttpRequestDeserializer.Deserialize<DeleteContactRequestCommand>(requestBody, unsupportedFormat));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.ContentType.Should().Be(unsupportedFormat);
        }

        [Fact(DisplayName = "Request body not provided")]
        [Trait("Action", "Deserialize")]
        public void Deserialize_RequestBodyNotProvided_ShouldReturnCommandDefaultInstance()
        {
            // Arrange
            string requestBody = string.Empty;
            string format = "application/json";

            // Act
            DeleteContactRequestCommand? deserializedCommand = HttpRequestDeserializer.Deserialize<DeleteContactRequestCommand>(requestBody, format);

            // Assert
            deserializedCommand.Should().Be(default(DeleteContactRequestCommand));
        }

        [Fact(DisplayName = "Content type not provided")]
        [Trait("Action", "Deserialize")]
        public void Deserialize_ContentTypeNotProvided_ShouldThrowHttpResponseDeserializerException()
        {
            // Arrange
            DeleteContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
            };
            string requestBody = JsonSerializer.Serialize(command);
            string? format = null;

            // Assert
            HttpResponseDeserializerException exception = Assert.Throws<HttpResponseDeserializerException>(() => HttpRequestDeserializer.Deserialize<DeleteContactRequestCommand>(requestBody, format));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.ContentType.Should().Be(format);
        }
    }
}