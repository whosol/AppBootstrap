using System;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.MailboxPlugin.Bootstrapper;

namespace Actemium.Stratus.Mailbox
{
    public class MailboxPlugin : PluginBase
    {
        public override string Description
        {
            get { return "Mailbox plugin for service controller"; }
        }

        public override void Start()
        {
            Bootstrapper.Start();
        }
        public override void Stop()
        {
            Bootstrapper.Stop();
        }
        public MailboxPlugin(IConfiguration configuration)
            : base(configuration)
        {

        }
    }
}
