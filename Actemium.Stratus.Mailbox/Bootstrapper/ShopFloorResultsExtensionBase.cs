using System;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Bootstrapper
{
    public abstract class ShopFloorResultsExtensionBase : IShopFloorResultsExtension
    {
        #region IDescribeable Interface

        public string Name
        {
            get { return this.GetType().FullName.ToString(); }
        }

        public abstract string Describe(); 
        
        #endregion
        
        #region Public Virtual

        public abstract void Start();

        public abstract void Stop();

        #endregion
    }
}
