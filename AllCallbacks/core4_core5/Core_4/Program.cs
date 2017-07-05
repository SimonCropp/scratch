using System;
using System.Threading;
using Messages;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Installation.Environments;
using NServiceBus.Unicast;

class Program
{
    public static void Main()
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));
        var synchronizationContext = SynchronizationContext.Current;
        using (var bus = CreateBus())
        {
            SynchronizationContext.SetSynchronizationContext(synchronizationContext);
            var asyncResult = bus.Send("Core_5", new ObjectMessage())
                .Register(i =>
                {
                    var localResult = (CompletionResult) i.AsyncState;
                    var response = (ObjectResponseMessage) localResult.Messages[0];
                    Console.WriteLine(response.Property);
                }, null);
            asyncResult.AsyncWaitHandle.WaitOne();
        }
    }

    static UnicastBus CreateBus()
    {
        Configure.GetEndpointNameAction = () => "Core_4";

        Logging.ConfigureLogging();
        Configure.Features.Disable<TimeoutManager>();
        var configure = Configure.With();
        configure.PurgeOnStartup(true);
        configure.DefiningMessagesAs(MessageConventions.IsMessage);
        configure.DefaultBuilder();
        configure.UseTransport<Msmq>();

        return (UnicastBus) configure.UnicastBus()
            .CreateBus().Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
    }
}