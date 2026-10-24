# Howest Prime Movies Microservice

The Movies service manages the movie catalog, cinema rooms, movie events and showtimes, and booking capacity. It exposes an HTTP API for catalog and event operations and exchanges integration events with the Ticketing service over AMQP.

## Responsibilities

- Register movies, find a movie by ID, search by title or genre, and change movie details.
- Schedule movie events, search events by month, and expose the Howest Prime schedule.
- Open bookings for movie events and respond to payment success or failure events from Ticketing.
- Publish movie registration, movie detail change, and booking-opened events.

The HTTP contract is described in [config/webapi.yaml](config/webapi.yaml). The message contract is described in [config/asyncapi.yaml](config/asyncapi.yaml).

## Architecture and technology

The solution is divided into Main, Infrastructure, Application, Domain, and Shared projects. It uses .NET 10, ASP.NET Core, Entity Framework Core, PostgreSQL, and AMQP messaging. The local dependency compose file defines PostgreSQL and LavinMQ services.

## Development

Prerequisites: .NET 10 SDK and Docker Compose for the local database and message broker. The compose configuration is at [config/movies-howestprime-dev/docker-compose.yml](config/movies-howestprime-dev/docker-compose.yml); it references a local database environment file, so provide the required local configuration before starting dependencies.

Run the unit tests with:

```sh
dotnet test test/UnitTests/UnitTests.csproj
```

GitHub Actions also runs unit tests and coverage checks and contains container build and Azure Container Apps deployment jobs. Deployment requires the repository’s configured GitHub variables and secrets.

## Related repositories

- [Android client](https://github.com/Howest-Prime/st-client-mobile)
- [Ticketing microservice](https://github.com/Howest-Prime/st-microservice-ticketing)
