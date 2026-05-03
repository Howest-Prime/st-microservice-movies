
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents.Exceptions;
using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.Events;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record CloseBookingInput(
    Guid BookingId,
    string CloseBookingReason
);



public sealed class CloseBooking(
    IUnitOfWork unitOfWork,
    ILogger<CloseBooking> logger
): IUseCase<CloseBookingInput>
{
    public async Task Execute(CloseBookingInput input)
    {
        BookingID bookingId = EntityId.New<BookingID>(input.BookingId);
        MovieEvent? movieEvent = await unitOfWork.Repo<IMovieEventRepository>().ByBookingId(bookingId) 
                    ?? throw new InvalidBookingException(bookingId.Value);
        
        PaymentStatus paymentStatus = MapCloseBookingReasonToPaymentStatus(input.CloseBookingReason);
        movieEvent.CloseBooking(bookingId, paymentStatus);

        await unitOfWork.Do();        
        logger.LogInformation("Closed booking with ID {BookingId} for movie event with ID {MovieEventId} due to reason: {Reason}", 
            bookingId.Value, movieEvent.Id.Value, input.CloseBookingReason);

    }

    private PaymentStatus MapCloseBookingReasonToPaymentStatus(string reason)
    {
        return reason.ToLower() switch
        {
            "payment failed" => PaymentStatus.Failed,
            "payment successful" => PaymentStatus.Success,
            _ => throw new InvalidCloseBookingReasonException(reason)
        };
    }
}