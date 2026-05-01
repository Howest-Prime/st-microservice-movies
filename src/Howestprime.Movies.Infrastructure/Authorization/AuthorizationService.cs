using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.Authorization.Configuration;
using Microsoft.Extensions.Options;

namespace Howestprime.Movies.Infrastructure.Authorization;

public sealed class AuthorizationService(
    IOptions<AuthorizationOption> options
) : IAuthorizationService
{
    public Task Authorize(string role, string useCaseName)
    {
        string normalizedRole = role.Trim();

        IList<string>? permissions = options.Value.PermissionsByRole
            .FirstOrDefault(r => r.Name.Equals(normalizedRole, StringComparison.OrdinalIgnoreCase))
            ?.Permissions;
            
        if (permissions == null || !permissions.Contains(useCaseName, StringComparer.OrdinalIgnoreCase))
        {
            throw new UnauthorizedAccessException(
                $"Role '{role}' is not allowed to execute this functionality.");
        }

        return Task.CompletedTask;
    }
}