using Services.Constants;

namespace Services.Exceptions;

public abstract class BaseException : Exception
{
    public ErrorCodes Code { get; protected set; }
    
    protected BaseException(ErrorCodes code)
    {
        Code = code;
    }

    protected BaseException(ErrorCodes code, string message) 
    {
        Code = code;
    }

    protected BaseException(ErrorCodes code, string message, Exception innerException) 
    {
        Code = code;
    }
    
}
