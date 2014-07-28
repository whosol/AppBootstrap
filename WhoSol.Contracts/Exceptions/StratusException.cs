using WhoSol.Contracts.Enums;
using System;

namespace WhoSol.Contracts.Exceptions
{
    public class StratusException : ApplicationException
    {
        public ErrorLevel ErrorLevel { get; private set; }
        public StratusException(string message, ErrorLevel errorLevel, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorLevel = errorLevel;
        }
    }
}
