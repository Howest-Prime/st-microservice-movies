
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Infrastructure.Messaging.IntegrationEvents.Messages;
using Howestprime.Movies.Infrastructure.Messaging.IntegrationEvents.Shared.Contracts;
using Howestprime.Movies.Infrastructure.Messaging.IntegrationEvents.Shared.Messages;

namespace Howestprime.Movies.Infrastructure.Messaging.IntegrationEvents.Movies.Controllers;

public sealed class WhenPaymentSuccessfulCloseBookingController(
    IUseCase<CloseBookingInput> useCase
) : IController<ConsumerContext>
{
    public async Task Handle(ConsumerContext context)
    {
        PaymentSuccessfulMessage message = AmqpMessageConverter.ParseBody<PaymentSuccessfulMessage>(context);
        CloseBookingInput input = new CloseBookingInput(
            message.BookingId,
            "Payment successful"
        );

        await useCase.Execute(input);
    }
       
}