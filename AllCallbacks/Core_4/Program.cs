using System.Threading;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Installation.Environments;
using NServiceBus.Unicast;

class Program
{
    public static void Main()
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
        Configure.GetEndpointNameAction = () => "Core_4";

        Logging.ConfigureLogging();
        Configure.Features.Disable<TimeoutManager>();
        Configure.Serialization.Json();
        var configure = Configure.With();
        configure.DefiningMessagesAs(MessageConventions.IsMessage);
        configure.DefaultBuilder();
        configure.UseTransport<Msmq>();

        return (UnicastBus) configure.UnicastBus()
            .CreateBus().Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
    }
}