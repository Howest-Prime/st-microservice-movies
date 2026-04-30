namespace Howestprime.Movies.Domain.MovieEvent.ValueObject;

public enum PaymentStatusState
{
    Unpaid,
    Paid,
    Refunded
}

public sealed class PaymentStatus 
{
    public PaymentStatusState State { get; private init; }

    private PaymentStatus(
        PaymentStatusState state
    )
    {
        State = state;
    } 

    public static PaymentStatus Unpaid   => new PaymentStatus(PaymentStatusState.Unpaid);
    public static PaymentStatus Paid     => new PaymentStatus(PaymentStatusState.Paid);
    public static PaymentStatus Refunded => new PaymentStatus(PaymentStatusState.Refunded);
}