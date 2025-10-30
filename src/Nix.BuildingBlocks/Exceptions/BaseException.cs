namespace Nix.BuildingBlocks.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException(string message) : base(message) { }
    protected BaseException(string message, Exception innerException) : base(message, innerException) { }
}

public class DomainException : BaseException
{
    public DomainException(string message) : base(message) { }
    public DomainException(string message, Exception innerException) : base(message, innerException) { }
}

public class ValidationException : BaseException
{
    public ValidationException(string message) : base(message) { }
    public ValidationException(string message, Exception innerException) : base(message, innerException) { }
}

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
}

public class ConflictException : BaseException
{
    public ConflictException(string message) : base(message) { }
    public ConflictException(string message, Exception innerException) : base(message, innerException) { }
}

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(string message) : base(message) { }
    public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
}

public class ForbiddenException : BaseException
{
    public ForbiddenException(string message) : base(message) { }
    public ForbiddenException(string message, Exception innerException) : base(message, innerException) { }
} 
