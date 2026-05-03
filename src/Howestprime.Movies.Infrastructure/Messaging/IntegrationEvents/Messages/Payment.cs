
namespace Howestprime.Movies.Infrastructure.Messaging.IntegrationEvents.Messages;

public sealed record PaymentFailedMessage(
    Guid BookingId
);

public sealed record PaymentSuccessfulMessage(
    Guid BookingId
);

