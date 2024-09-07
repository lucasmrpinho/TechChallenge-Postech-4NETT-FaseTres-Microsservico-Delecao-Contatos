using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Inputs;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Outputs;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Interfaces
{
    public interface IDeleteContactUseCase
    {
        Task<DeleteContactOutput> ExecuteAsync(DeleteContactInput input);
    }
}