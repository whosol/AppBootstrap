using System;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Parse
{
    public struct Rds1Result
    {
        #region Properties

        public string ResultType { get; set; }
        public string Name { get; set; }
        public string Value { get; set; } 

        #endregion
    }
}
