namespace Nix.BuildingBlocks;

public class Result<T>
{
    public T? Value { get; private set; }
    public bool IsSuccess { get; private set; }
    public string? Error { get; private set; }
    public Exception? Exception { get; private set; }

    protected Result(T value)
    {
        Value = value;
        IsSuccess = true;
    }

    protected Result(string error, Exception? exception = null)
    {
        Error = error;
        Exception = exception;
        IsSuccess = false;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(string error, Exception? exception = null) => new(error, exception);

    public static implicit operator Result<T>(T value) => Success(value);
}

public class Result
{
    public bool IsSuccess { get; private set; }
    public string? Error { get; private set; }
    public Exception? Exception { get; private set; }

    protected Result(bool isSuccess, string? error = null, Exception? exception = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        Exception = exception;
    }

    public static Result Success() => new(true);
    public static Result Failure(string error, Exception? exception = null) => new(false, error, exception);

    public static Result<T> Success<T>(T value) => Result<T>.Success(value);
    public static Result<T> Failure<T>(string error, Exception? exception = null) => Result<T>.Failure(error, exception);
} 
