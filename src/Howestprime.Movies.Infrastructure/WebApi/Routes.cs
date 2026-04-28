using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Howestprime.Movies.Infrastructure.WebApi.Controllers;

namespace Howestprime.Movies.Infrastructure.WebApi;

public static class Routes
{
    public static IEndpointRouteBuilder MapRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder webApi = app.MapGroup("/api");

        webApi.MapMovieRoutes();

        return app;
    }

    private static RouteGroupBuilder MapMovieRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder movieCatalog = app.MapGroup("/movie-catalog")
            .WithTags("Movie Catalog")
            .WithOpenApi();
        
        movieCatalog.MapPost("/", RegisterMovieController.Invoke)
            .WithName("RegisterMovie")
            .WithDescription("Register a new movie.");

        return movieCatalog;
    }
}