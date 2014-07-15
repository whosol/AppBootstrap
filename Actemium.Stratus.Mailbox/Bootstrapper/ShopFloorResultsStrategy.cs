using Appccelerate.Bootstrapper;
using System;
using System.Linq;
using Appccelerate.Bootstrapper.Syntax;

namespace Actemium.Stratus.MailboxPlugin.Bootstrapper
{
    public class ShopFloorResultsStrategy : AbstractStrategy<IShopFloorResultsExtension>
    {
        #region Protected Overrides

        protected override void DefineRunSyntax(ISyntaxBuilder<IShopFloorResultsExtension> builder)
        {
            builder.Execute(extension => extension.Start());
        }

        protected override void DefineShutdownSyntax(ISyntaxBuilder<IShopFloorResultsExtension> builder)
        {
            builder.Execute(extension => extension.Stop());
        } 

        #endregion
    }
}
