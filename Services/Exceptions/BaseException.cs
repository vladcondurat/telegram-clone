using Services.Constants;

namespace Services.Exceptions;

public abstract class BaseException : Exception
{
    public ErrorCodes ErrorCode { get; }

    protected BaseException(ErrorCodes errorCode) : base() 
    { 
        ErrorCode = errorCode;
    }
    protected BaseException(ErrorCodes errorCode, string message) : base(message) 
    { 
        ErrorCode = errorCode;
    }

    protected BaseException(ErrorCodes errorCode, string message, Exception innerException) : base(message, innerException)
    { 
        ErrorCode = errorCode;
    }
}
