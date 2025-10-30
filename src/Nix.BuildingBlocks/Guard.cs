namespace Nix.BuildingBlocks;

public static class Guard
{
    public static void Against(bool condition, string message)
    {
        if (condition)
            throw new ArgumentException(message);
    }

    public static void AgainstNull(object? value, string paramName)
    {
        if (value is null)
            throw new ArgumentNullException(paramName);
    }

    public static void AgainstNullOrEmpty(string? value, string paramName)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException($"Value cannot be null or empty", paramName);
    }

    public static void AgainstNullOrWhiteSpace(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"Value cannot be null or whitespace", paramName);
    }

    public static void AgainstOutOfRange(int value, int min, int max, string paramName)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(paramName, value, $"Value must be between {min} and {max}");
    }

    public static void AgainstLengthGreaterThan(string value, int maxLength, string paramName)
    {
        if (value.Length > maxLength)
        {
            throw new ArgumentOutOfRangeException(value, paramName, $"Value cannot be longer than {maxLength} characters");
        }
    }

    public static void AgainstNegative(decimal value, string paramName)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(paramName, value, "Value cannot be negative");
    }

    public static void AgainstNegativeOrZero(int value, string paramName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(paramName, value, "Value cannot be negative or zero");
    }

    public static void AgainstEmpty(Guid value, string paramName)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Value cannot be empty GUID", paramName);
    }
} 
