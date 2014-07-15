using System;
using System.Linq;
using Appccelerate.Bootstrapper;

namespace Actemium.Stratus.MailboxPlugin.Bootstrapper
{
    public interface IShopFloorResultsExtension : IExtension
    {
        void Start();
        void Stop();
    }
}
