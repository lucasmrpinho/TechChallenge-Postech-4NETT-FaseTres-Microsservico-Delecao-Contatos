using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.Entities;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.ValueObjects;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Extensions
{
    [ExcludeFromCodeCoverage]
    internal static class ContactEntityExtensions
    {
        internal static ContactDeletedEvent AsContactDeletedEvent(this ContactEntity contactEntity)
        {
            return new()
            {
                ContactId = contactEntity.Id,
            };
        }
    }
}