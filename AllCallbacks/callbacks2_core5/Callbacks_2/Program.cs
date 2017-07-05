using System;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;
using Messages;

class Program
{

    public static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));
        var bus = await CreateBus()
            .ConfigureAwait(false);
        var sendOptions = new SendOptions();
        sendOptions.SetDestination("Core_5");
        var response = await bus.Request<ObjectResponseMessage>(new ObjectMessage(), sendOptions).ConfigureAwait(false);
        Console.Read();
        await bus.Stop().ConfigureAwait(false);
    }

    static Task<IEndpointInstance> CreateBus()
    {
        var endpointConfiguration = new EndpointConfiguration("Callbacks_2");
        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningMessagesAs(MessageConventions.IsMessage);

        endpointConfiguration.PurgeOnStartup(true);
        endpointConfiguration.DisableFeature<TimeoutManager>();
        endpointConfiguration.SendFailedMessagesTo("error");
        var recoverability = endpointConfiguration.Recoverability();
#pragma warning disable 618
        recoverability.DisableLegacyRetriesSatellite();
#pragma warning restore 618
        endpointConfiguration.UseTransport<MsmqTransport>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        // Required by callbacks to have each instance uniquely addressable
        endpointConfiguration.MakeInstanceUniquelyAddressable("1");
        endpointConfiguration.EnableCallbacks();
        endpointConfiguration.EnableInstallers();

        return Endpoint.Start(endpointConfiguration);
    }

}