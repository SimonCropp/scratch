using System;
using System.Threading;
using NServiceBus;
using NServiceBus.Installation.Environments;
using NServiceBus.Unicast;

class Program
{
    static void Main()
    {
        var synchronizationContext = SynchronizationContext.Current;
        using (CreateBus())
        {
            SynchronizationContext.SetSynchronizationContext(synchronizationContext);
            Console.Read();
        }
    }

    static UnicastBus CreateBus()
    {
        Configure.GetEndpointNameAction = () => "Core_3";

        Logging.ConfigureLogging();
        var configure = Configure.With();
        configure.PurgeOnStartup(true);
        configure.DisableTimeoutManager();
        configure.DefiningMessagesAs(MessageConventions.IsMessage);
        configure.DefaultBuilder();
        configure.MsmqTransport();

        return (UnicastBus) configure.UnicastBus()
            .CreateBus().Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
    }
}