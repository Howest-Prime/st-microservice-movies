namespace Howestprime.Movies.Infrastructure.Authorization.Configuration;

public class AuthorizationOption
{
    public IList<RolePermission> PermissionsByRole { get; set; } = [];
}