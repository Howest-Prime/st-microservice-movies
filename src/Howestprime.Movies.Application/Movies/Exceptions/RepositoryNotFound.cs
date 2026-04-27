using Howestprime.Movies.Application.Shared;

namespace Howestprime.Movies.Application.Movies.Exceptions;

public class RepositoryNotFoundException : MovieApplicationException
{
    public RepositoryNotFoundException(string repositoryName) : base($"The repository '{repositoryName}' was not found.")
    {
    }
}