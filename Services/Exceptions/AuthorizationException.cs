using Services.Constants;

namespace Services.Exceptions;

public sealed class AuthorizationException : BaseException
{
    
    public AuthorizationException() : base(ErrorCodes.GenericError) { }

    public AuthorizationException(string message) : base(ErrorCodes.GenericError, message) { }
    
    public AuthorizationException(string message, Exception innerException) : base(ErrorCodes.GenericError, message, innerException) { }

    public AuthorizationException(ErrorCodes code, string message, Exception innerException) : base(code, message, innerException) { }

    public AuthorizationException(ErrorCodes code, string message) : base(code, message) { }

    public AuthorizationException(ErrorCodes code) : base(code) { }

}