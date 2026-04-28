using Aornis;
using Howestprime.Movies.Application.Contracts.Data;
using System.Linq.Expressions;

namespace Howestprime.Movies.Application.Contracts.Ports;

public interface ISearchMovieCatalogQuery
{
    public  Task<Optional<IReadOnlyList<MovieData>>> Fetch(
        Expression<Func<MovieData, bool>>? filter = null
    );
}