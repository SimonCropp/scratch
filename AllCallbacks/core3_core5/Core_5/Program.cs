using System;
using NServiceBus;
using NServiceBus.Features;

class Program
{
    public static void Main()
    {
        using (CreateBus())
        {
            Console.Read();
        }
    }

    static IBus CreateBus()
    {
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Core_5");
        busConfiguration.PurgeOnStartup(true);
        var conventions = busConfiguration.Conventions();
        conventions.DefiningMessagesAs(MessageConventions.IsMessage);
        busConfiguration.UseTransport<MsmqTransport>();
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.DisableFeature<TimeoutManager>();
        busConfiguration.EnableInstallers();
        var startableBus = Bus.Create(busConfiguration);
        return startableBus.Start();
    }

}