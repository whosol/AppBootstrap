using Actemium.Stratus.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actemium.Stratus.Contracts.Exceptions
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
