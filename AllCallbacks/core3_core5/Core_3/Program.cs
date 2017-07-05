using System;
using System.Threading;
using Messages;
using NServiceBus;
using NServiceBus.Installation.Environments;
using NServiceBus.Unicast;

class Program
{
    static void Main()
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));
        var synchronizationContext = SynchronizationContext.Current;
        using (var bus = CreateBus())
        {
            SynchronizationContext.SetSynchronizationContext(synchronizationContext);
            var asyncResult = ((IBus) bus).Send("Core_5", new ObjectMessage())
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