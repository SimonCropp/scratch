
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NServiceBus;

class Program
{
    static void Main()
    {
        var endpointConfiguration = new EndpointConfiguration("NewtonsoftSerializerSample");
        endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.UseTransport<LearningTransport>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        Run(endpointConfiguration).GetAwaiter().GetResult();
    }

    static async Task Run(EndpointConfiguration endpointConfiguration)
    {
        var endpoint = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

        await endpoint.Publish<MyEvent>(
                message => message.Changes = JObject.Parse(@"
{
    'id': '814efd4f-f20d-45f9-8efd-4ff20db5f9ca',
    'edit': {
        'title': '123123',
        'description': 'abcabc'
    }
}"))
            .ConfigureAwait(false);

        var myMessage = new MyMessage
        {
            Changes = JObject.Parse(@"
{
    'id': '814efd4f-f20d-45f9-8efd-4ff20db5f9ca',
    'edit': {
        'title': '123123',
        'description': 'abcabc'
    }
}")
        };
        await endpoint.SendLocal(myMessage)
            .ConfigureAwait(false);

        Console.WriteLine("\r\nPress any key to stop program\r\n");
        Console.Read();
    }
}