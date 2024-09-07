using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.Primitives;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Serializers;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Serializers.Exceptions;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Serializers.Results;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.UnitTests.Suite.Infra.Http.Serializers
{
    public class HttpResponseSerializerTests
    {        
        [Theory(DisplayName = "Serialize a valid response command in application json format")]
        [InlineData("application/json")]
        [InlineData("application/*")]
        [InlineData("*/*")]
        [Trait("Action", "Serialize")]
        public void Serialize_SerializeValidResponseCommandInApplicationJsonFormat_ShouldSerializeResponseCommand(string acceptHeader)
        {
            // Arrange
            DeleteContactResponseCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                ContactNotifiedForDeleteAt = DateTime.UtcNow,
                IsContactNotifiedForDelete = true
            };

            // Act
            HttpResponseSerializeResult result = HttpResponseSerializer.Serialize(command, acceptHeader);

            // Assert
            result.ContentType.Should().Be("application/json");
            result.Data.Should().NotBeNullOrEmpty();
            result.Data.Should().Be(JsonSerializer.Serialize(command));
        }

        [Fact(DisplayName = "Serialize a valid response command into an unsupported format")]
        [Trait("Action", "Serialize")]
        public void Serialize_SerializeValidResponseCommandIntoAnUnsupportedFormat_ShouldThrowHttpResponseSerializerException()
        {
            // Arrange
            DeleteContactResponseCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                ContactNotifiedForDeleteAt = DateTime.UtcNow,
                IsContactNotifiedForDelete = true
            };
            StringValues acceptHeader = "application/xml";

            // Assert
            HttpResponseSerializerException exception = Assert.Throws<HttpResponseSerializerException>(() => HttpResponseSerializer.Serialize(command, acceptHeader));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.AcceptHeaderValues.Should().BeEquivalentTo(acceptHeader);
        }

        [Fact(DisplayName = "Serialize a valid response command in application json format")]
        [Trait("Action", "Serialize")]
        public void Serialize_AcceptHeaderNotProvided_ShouldSerializeResponseCommandIntoApplicationJsonFormat()
        {
            // Arrange
            DeleteContactResponseCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                IsContactNotifiedForDelete = false
            };
            StringValues acceptHeader = [];

            // Act
            HttpResponseSerializeResult result = HttpResponseSerializer.Serialize(command, acceptHeader);

            // Assert
            result.ContentType.Should().Be("application/json");
            result.Data.Should().NotBeNullOrEmpty();
            result.Data.Should().Be(JsonSerializer.Serialize(command));
        }
    }
}