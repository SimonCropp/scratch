using System;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        Console.Title = "Samples.SqlPersistence.Client";
        var endpointConfiguration = new EndpointConfiguration("Samples.SqlPersistence.Client");
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        var transport = endpointConfiguration.UseTransport<MsmqTransport>();
        endpointConfiguration.SendFailedMessagesTo("error");
        var routing = transport.Routing();
        routing.RegisterPublisher(
            eventType: typeof(OrderCompleted),
            publisherEndpoint: "Samples.SqlPersistence.EndpointSqlServer");

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

        Console.WriteLine("Press any key to exit");
        Console.Read();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}