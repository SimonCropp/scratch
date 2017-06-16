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
        Console.Title = "SqlServerSendRepro.Sender";
        var endpointConfiguration = new EndpointConfiguration("SqlServerSendRepro.Sender");
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        endpointConfiguration.EnableInstallers();

        var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
        transport.ConnectionString(@"Data Source=.\SqlExpress;Database=SqlServerSendRepro;Integrated Security=True");

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        var remoteMessage = new RemoteMessage
        {
            SendFromContext = "EndpointInstance"
        };
        Console.WriteLine("Hello from LocalHandler. Sending to Receiver from EndpointInstance");
        await endpointInstance.Send("SqlServerSendRepro.Receiver", remoteMessage)
            .ConfigureAwait(false);
        await endpointInstance.SendLocal(new LocalMessage())
            .ConfigureAwait(false);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }

}