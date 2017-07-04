using NServiceBus;
using NServiceBus.Features;

class Program
{
    public static void Main()
    {
        using (var bus = CreateBus())
        {
            bus.Initiate();
        }
    }

    static IBus CreateBus()
    {
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Core_5");
        var conventions = busConfiguration.Conventions();
        conventions.DefiningMessagesAs(MessageConventions.IsMessage);
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.UseTransport<MsmqTransport>();
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.DisableFeature<TimeoutManager>();
        busConfiguration.EnableInstallers();
        var startableBus = Bus.Create(busConfiguration);
        return startableBus.Start();
    }

}