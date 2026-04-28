
namespace Howestprime.Movies.Application.Contracts.Ports;


public enum Role
{
    Manager,
    User
}

public interface IAuthorizationService
{
    public Task Authorize(Role role, string useCaseName);
}