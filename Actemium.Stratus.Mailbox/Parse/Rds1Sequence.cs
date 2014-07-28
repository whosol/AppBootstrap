using System;
using System.Collections.Generic;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Parse
{
    public class Rds1Sequence
    {
        #region Properties

        public Rds1Header Header { get; set; }

        public List<Rds1Result> Results { get; set; } 
        
        #endregion
    }
}
