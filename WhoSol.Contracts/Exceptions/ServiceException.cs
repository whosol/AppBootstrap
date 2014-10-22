using WhoSol.Contracts.Enums;
using System;

namespace WhoSol.Contracts.Exceptions
{
    public class ServiceException : ApplicationException
    {
        public ErrorLevel ErrorLevel { get; private set; }
        public ServiceException(string message, ErrorLevel errorLevel, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorLevel = errorLevel;
        }
    }
}
