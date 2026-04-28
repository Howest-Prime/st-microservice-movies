namespace Howestprime.Movies.Infrastructure.Authorization.Configuration;

public class AuthorizationOption
{
    public List<RolePermission> PermissionsByRole { get; set; } = [];
}