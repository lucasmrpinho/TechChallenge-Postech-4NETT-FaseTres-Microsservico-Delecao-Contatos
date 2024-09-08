using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.Exceptions.Common;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Core.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class AreaCodeValueNotSupportedException(string message, string areaCodeValue) : DomainException(message)
    {
        public string AreaCodeValue { get; } = areaCodeValue;
    }
}