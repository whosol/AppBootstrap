using System;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Parse
{
    public struct Rds1Header
    {
        #region Properties

        public string Station { get; set; }
        
        public string ProductId { get; set; }
        
        public string Sequence { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public int Duration { get; set; }
        
        public string Flags { get; set; } 
        
        #endregion
    }
}
