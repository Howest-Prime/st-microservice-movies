namespace Howestprime.Movies.Infrastructure.Authorization.Configuration;

public class RolePermission 
{
    public string Name { get; set; } = string.Empty;
    public IList<string> Permissions { get; set; } = new List<string>();

}