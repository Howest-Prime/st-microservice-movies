

using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.Authorization.Configuration;
using Microsoft.Extensions.Options;

namespace Howestprime.Movies.Infrastructure.Authorization;

public class AuthorizationService(
    IOptions<AuthorizationOption> authorizationOptions
) : IAuthorizationService
{
    public Task Authorize(Role role, string useCaseName)
    {
        string normalizedUseCaseName = useCaseName.Trim();

        string[] allowedUseCases = authorizationOptions.Value.PermissionsByRole
            .FirstOrDefault(rp => rp.Name.Equals(role.ToString(), StringComparison.OrdinalIgnoreCase))
            ?.Permissions ?? [];

        if (!allowedUseCases.Any(permission =>
                permission.Equals(normalizedUseCaseName, StringComparison.OrdinalIgnoreCase)))
            throw new UnauthorizedAccessException($"Role '{role}' does not have permission to execute use case '{useCaseName}'.");

        return Task.CompletedTask;
    }
}