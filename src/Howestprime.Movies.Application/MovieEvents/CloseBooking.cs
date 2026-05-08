
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Shared.Exceptions;
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
        BookingId bookingId = EntityId.New<BookingId>(input.BookingId);
        MovieEvent? movieEvent = await unitOfWork.Repo<IMovieEventRepository>().ByBookingId(bookingId) 
                    ?? throw new NotFoundException($"Movie event with booking id {bookingId.Value} not found.");
        
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
            _ => throw new ArgumentException($"Invalid close booking reason: {reason}")
        };
    }
}