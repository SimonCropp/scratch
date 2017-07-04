using System.Threading;
using NServiceBus;
using NServiceBus.Installation.Environments;
using NServiceBus.Unicast;

class Program
{
    static void Main()
    {
        var synchronizationContext = SynchronizationContext.Current;
        using (var bus = CreateBus())
        {
            SynchronizationContext.SetSynchronizationContext(synchronizationContext);
            bus.Initiate();
        }
    }

    static UnicastBus CreateBus()
    {
        Configure.GetEndpointNameAction = () => "Core_3";

        Logging.ConfigureLogging();
        var configure = Configure.With();
        configure.DisableTimeoutManager();
        configure.DefiningMessagesAs(MessageConventions.IsMessage);
        configure.DefaultBuilder();
        configure.MsmqTransport();
        configure.JsonSerializer();

        return (UnicastBus) configure.UnicastBus()
            .CreateBus().Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
    }
}