using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.MovieEvents;


namespace Howestprime.Movies.Infrastructure.WebApi;

public static class Routes
{
    public static IEndpointRouteBuilder MapRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder webApi = app.MapGroup("/api");

        webApi.MapMovieRoutes();
        webApi.MapMovieEventRoutes();
        webApi.MapHowestPrimeScheduleRoutes();

        return app;
    }

    private static RouteGroupBuilder MapMovieRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder movieCatalog = app.MapGroup("/movie-catalog")
            .WithTags("Movie Catalog")
            .WithDescription("Endpoints for managing the movie catalog.");
            
        movieCatalog.MapPost("/", RegisterMovieController.Invoke)
            .WithName("RegisterMovie")
            .WithDescription("Register a new movie.");

        movieCatalog.MapGet("/", SearchMovieByTitleAndGenresController.Invoke)
            .WithName("SearchMovieByTitleAndGenres")
            .WithDescription("Search movies by title and genres.");

        movieCatalog.MapGet("/{id}", FindMovieByIDController.Invoke)
            .WithName("FindMovieByID")
            .WithDescription("Find a movie by its ID.");

        return movieCatalog;
    }

    private static RouteGroupBuilder MapMovieEventRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder movieEvents = app.MapGroup("/movie-events")
            .WithTags("Movie Events")
            .WithDescription("Endpoints for managing movie events.");

        movieEvents.MapPost("/", ScheduleMovieEventController.Invoke)
            .WithName("ScheduleMovieEvent")
            .WithDescription("Schedule a new movie event.");

        return movieEvents;
    }

    private static RouteGroupBuilder MapHowestPrimeScheduleRoutes(this RouteGroupBuilder app)
    {
        RouteGroupBuilder howestPrimeSchedule = app.MapGroup("/howestprime-schedule")
            .WithTags("Howest Prime Schedule")
            .WithDescription("Endpoints for managing the Howest Prime schedule.");

        howestPrimeSchedule.MapGet("/", GetHowestPrimeScheduleController.Invoke)
            .WithName("GetHowestPrimeSchedule")
            .WithDescription("Get the schedule of movie events for the next two weeks.");

        return howestPrimeSchedule;
    }
}