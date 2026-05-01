namespace Howestprime.Movies.Infrastructure.Authorization.Configuration;

public sealed class RolePermission 
{
    public string Name { get; init; } = string.Empty;
    public IList<string> Permissions { get; init; } = new List<string>();

}