using System;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Ninject.Extensions.Logging;
using Ninject;
using Actemium.Stratus.Contracts.Base;

namespace Actemium.Stratus.Mailbox
{
    public class MailboxPlugin : BasePlugin
    {
        private readonly IKernel kernel;

        public override string Description
        {
            get { return "Mailbox plugin for service controller"; }
        }

        public override void Start()
        {
            Bootstrapper.Start(kernel);
        }
        public override void Stop()
        {
            Bootstrapper.Stop();
        }
        public MailboxPlugin(ILogger logger, IConfiguration configuration, IKernel kernel)
            : base(logger, configuration)
        {
            this.kernel = kernel;
        }
    }
}
