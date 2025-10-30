namespace Nix.BuildingBlocks.Domain.ValueObjects;

public record struct Money(
    decimal Amount,
    string Currency
)
{
    public static Money  Rub(decimal amount) => new(amount, "RUB");
    
    public static Money Create(decimal value, string currency = "RUB")
    {
        Guard.AgainstNegative(value, nameof(value));

        return new Money(value, currency);
    }
}
