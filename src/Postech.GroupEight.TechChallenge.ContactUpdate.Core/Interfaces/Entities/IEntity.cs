namespace Postech.GroupEight.TechChallenge.ContactDelete.Core.Interfaces.Entities
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        DateTime? ModifiedAt { get; }
    }
}