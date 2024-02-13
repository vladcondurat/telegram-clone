using Services.Constants;

namespace Services.Exceptions;

public sealed class UserException : BaseException
{
    
    public UserException() : base(ErrorCodes.GenericError) { }

    public UserException(string message) : base(ErrorCodes.GenericError, message) { }
    
    public UserException(string message, Exception innerException) : base(ErrorCodes.GenericError, message, innerException) { }

    public UserException(ErrorCodes code, string message, Exception innerException) : base(code, message, innerException) { }

    public UserException(ErrorCodes code, string message) : base(code, message) { }

    public UserException(ErrorCodes code) : base(code) { }

}