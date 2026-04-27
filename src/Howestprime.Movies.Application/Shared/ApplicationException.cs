namespace Howestprime.Movies.Application.Shared;
public class MovieApplicationException : Exception
{
    public MovieApplicationException(string message) : base(message)
    {
    }
}