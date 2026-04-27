using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

 public enum AgeRatingValue
{
    All = 0,
    Six = 6,
    Nine = 9,
    Twelve = 12,
    Fourteen = 14,
    Sixteen = 16,
    Eighteen = 18
}

public record AgeRating : ValueObject
{
    public AgeRatingValue Value { get; private init; }

    private AgeRating(AgeRatingValue value)
    {
        Value = value;
    }

    public static AgeRating Create(AgeRatingValue value)
    {
        return new AgeRating(value);
    }

    public static AgeRating ALL => new AgeRating(AgeRatingValue.All);
    public static AgeRating SIX => new AgeRating(AgeRatingValue.Six);
    public static AgeRating NINE => new AgeRating(AgeRatingValue.Nine);
    public static AgeRating TWELVE => new AgeRating(AgeRatingValue.Twelve);
    public static AgeRating FOURTEEN => new AgeRating(AgeRatingValue.Fourteen);
    public static AgeRating SIXTEEN => new AgeRating(AgeRatingValue.Sixteen);
    public static AgeRating EIGHTEEN => new AgeRating(AgeRatingValue.Eighteen);

}