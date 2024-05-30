using Services.Constants;

namespace Services.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException() : base(ErrorCodes.BusinessBase) { }

        public BusinessException(string message) : base(ErrorCodes.BusinessBase, message) { }

        public BusinessException(string message, Exception innerException) : base(ErrorCodes.BusinessBase, message, innerException) { }

        public BusinessException(ErrorCodes code, string message, Exception innerException) : base(code, message, innerException) { }

        public BusinessException(ErrorCodes code, string message) : base(code, message) { }

        public BusinessException(ErrorCodes code) : base(code) { }
    }
}