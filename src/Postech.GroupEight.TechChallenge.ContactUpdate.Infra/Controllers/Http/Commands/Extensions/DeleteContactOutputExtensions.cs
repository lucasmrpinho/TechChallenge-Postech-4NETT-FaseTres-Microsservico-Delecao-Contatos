using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Outputs;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands.Extensions
{
    [ExcludeFromCodeCoverage]
    internal static class DeleteContactOutputExtensions
    {
        public static DeleteContactResponseCommand AsDeleteContactResponseCommand(this DeleteContactOutput deleteContactOutput)
        {
            return new()
            {
                ContactId = deleteContactOutput.ContactId,
                ContactNotifiedForDeleteAt = deleteContactOutput.ContactNotifiedForDeleteAt,
                IsContactNotifiedForDelete = deleteContactOutput.IsContactNotifiedForDelete,
            };
        }
    }
}