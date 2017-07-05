using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;

class Program
{

    public static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        var bus = await CreateBus()
            .ConfigureAwait(false);
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