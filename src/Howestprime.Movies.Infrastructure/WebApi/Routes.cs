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
            .WithTags("Movie, Movies and Movie Catalog")
            .WithDescription("Endpoints for managing the movie catalog.");
            
        movieCatalog.MapPost("/", RegisterMovieController.Invoke)
            .WithName("RegisterMovie")
            .WithDescription("Register a new movie in the catalog.");

        movieCatalog.MapGet("/", SearchMovieByTitleAndGenresController.Invoke)
            .WithName("SearchMovieByTitleAndGenres")
            .WithDescription("Search movies by title and/or genres.");

        movieCatalog.MapGet("/{movieId}", FindMovieByIdController.Invoke)
            .WithName("FindMovieById")
            .WithDescription("Find a movie by its ID.");
        movieCatalog.MapPut("/{movieId}", ChangeMovieDetailsController.Invoke)
            .WithName("ChangeMovieDetails")
            .WithDescription("Change the details of a movie in the catalog.");

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

        movieEvents.MapGet("/" , FindAllMovieEventsByMonthController.Invoke)
            .WithName("MovieEvent By Month")
            .WithDescription("Get list of movie events by year and month");

        movieEvents.MapPost("/{movieEventId}/booking", BookAMovieEventController.Invoke)
            .WithName("Book MovieEvent")
            .WithDescription("Make Booking for a Movie Event");

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