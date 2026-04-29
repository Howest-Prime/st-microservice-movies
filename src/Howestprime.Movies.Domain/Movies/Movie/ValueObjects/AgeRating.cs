using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

 public enum AgeRatingCategory
{
    All = 0,
    Six = 6,
    Nine = 9,
    Twelve = 12,
    Fourteen = 14,
    Sixteen = 16,
    Eighteen = 18,
}

public sealed record AgeRating : ValueObject
{
    public AgeRatingCategory Value { get; private init; }

    private AgeRating(
        AgeRatingCategory value
    )
    {
        Value = value;
    }

    public static AgeRating Create(
        AgeRatingCategory value
    )
    {
        return new AgeRating(value);
    }

    public static AgeRating From(
        int value
    )
    {
        return value switch {
            0 => ALL,
            6 => SIX,
            9 => NINE,
            12 => TWELVE,
            14 => FOURTEEN,
            16 => SIXTEEN,
            18 => EIGHTEEN,
            _ => throw new InvalidAgeRatingException(value.ToString(), Enum.GetNames<AgeRatingCategory>())
        };
    }
    
    public static AgeRating ALL => new AgeRating(AgeRatingCategory.All);
    public static AgeRating SIX => new AgeRating(AgeRatingCategory.Six);
    public static AgeRating NINE => new AgeRating(AgeRatingCategory.Nine);
    public static AgeRating TWELVE => new AgeRating(AgeRatingCategory.Twelve);
    public static AgeRating FOURTEEN => new AgeRating(AgeRatingCategory.Fourteen);
    public static AgeRating SIXTEEN => new AgeRating(AgeRatingCategory.Sixteen);
    public static AgeRating EIGHTEEN => new AgeRating(AgeRatingCategory.Eighteen);

}