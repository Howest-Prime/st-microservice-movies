using Howestprime.Movies.Main.Modules.Application;
using Howestprime.Movies.Main.Modules.Messaging.DomainEvents;
using Howestprime.Movies.Main.Modules.Messaging.IntegrationEvents;
using Howestprime.Movies.Main.Modules.Persistence.EntityFramework;
using Howestprime.Movies.Main.Modules.WebApi;
using Howestprime.Movies.Infrastructure.Authorization;
using Howestprime.Movies.Infrastructure.Authorization.Configuration;
using Howestprime.Movies.Application.Contracts.Ports;

namespace Howestprime.Movies.Main.Modules;

public static class AuthorizationModule
{

    public static IServiceCollection AddAuthorizationModule(
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        services
            .Configure<AuthorizationOption>(configuration.GetSection("Authorization"));

        services.AddScoped<IAuthorizationService, AuthorizationService>();

        return services;
    }


   
    

   
}