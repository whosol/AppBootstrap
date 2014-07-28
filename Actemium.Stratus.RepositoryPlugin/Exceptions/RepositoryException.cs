using WhoSol.Contracts.Enums;
using WhoSol.Contracts.Exceptions;
using System;

namespace Actemium.Stratus.RepositoryPlugin
{
    public class RepositoryException : StratusException
    {
        public RepositoryException(string message, ErrorLevel errorLevel, Exception innerException)
            : base(message, errorLevel, innerException)
        {

        }
    }
}
