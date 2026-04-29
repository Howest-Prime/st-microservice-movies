
namespace Howestprime.Movies.Application.Contracts.Ports;

public interface IAuthorizationService
{
    public Task Authorize(string role, string useCaseName);
}