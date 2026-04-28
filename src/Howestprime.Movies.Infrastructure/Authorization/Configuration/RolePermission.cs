namespace Howestprime.Movies.Infrastructure.Authorization.Configuration;

public class RolePermission
{
    public string Name { get; set; }
    public string[] Permissions { get; set; }

    public RolePermission(string name, string[] permissions)
    {
        Name = name;
        Permissions = permissions;
    }

    public RolePermission()
    {
        Name = string.Empty;
        Permissions = Array.Empty<string>();
    }
}