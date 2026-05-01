namespace Howestprime.Movies.Infrastructure.Authorization.Configuration;

public sealed class AuthorizationOption
{
    public IList<RolePermission> PermissionsByRole { get; init; } = [];
}