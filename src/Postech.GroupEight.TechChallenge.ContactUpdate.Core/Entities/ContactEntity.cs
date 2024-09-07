using Postech.GroupEight.TechChallenge.ContactDelete.Core.ValueObjects;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Core.Entities
{
    public class ContactEntity : EntityBase
    {
        public ContactEntity(Guid id) : base(id) { }

        private ContactEntity() { }

        public ContactNameValueObject ContactName { get; private set; }

        public ContactEmailValueObject ContactEmail { get; private set; } 

        public ContactPhoneValueObject ContactPhone { get; private set; }

    }
}