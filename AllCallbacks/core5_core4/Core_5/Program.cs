using System;
using System.Threading;
using Messages;
using NServiceBus;
using NServiceBus.Features;

class Program
{
    public static void Main()
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));
        using (var bus = CreateBus())
        {
            var asyncResult = bus.Send("Core_4", new ObjectMessage())
                .Register(i =>
                {
                    var localResult = (CompletionResult) i.AsyncState;
                    var response = (ObjectResponseMessage) localResult.Messages[0];
                    Console.WriteLine(response.Property);
                }, null);
            asyncResult.AsyncWaitHandle.WaitOne();
        }
    }

    static IBus CreateBus()
    {
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Core_5");
        var conventions = busConfiguration.Conventions();
        conventions.DefiningMessagesAs(MessageConventions.IsMessage);
        busConfiguration.UseTransport<MsmqTransport>();
        busConfiguration.PurgeOnStartup(true);
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.DisableFeature<TimeoutManager>();
        busConfiguration.EnableInstallers();
        var startableBus = Bus.Create(busConfiguration);
        return startableBus.Start();
    }

}