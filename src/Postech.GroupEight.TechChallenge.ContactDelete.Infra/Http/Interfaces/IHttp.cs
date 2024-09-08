using System.Net;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Interfaces
{
    public interface IHttp
    {
        void On<TRequest, TResponse>(string method, string url, Func<TRequest?, IDictionary<string, object?>, IServiceProvider, Task<GenericResponseCommand<TResponse>>> callback, int successfulStatusCode = (int) HttpStatusCode.Accepted, int failureStatusCode = (int) HttpStatusCode.BadRequest);
        void Run();
    }
}