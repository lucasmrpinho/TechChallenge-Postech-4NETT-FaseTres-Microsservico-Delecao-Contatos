using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.Entities;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.ValueObjects;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Inputs
{
    [ExcludeFromCodeCoverage]
    public record DeleteContactInput
    {
        public Guid ContactId { get; init; }

        internal ContactEntity AsContactEntity()
        {
            
            return new(ContactId);
        }

    }

 
}