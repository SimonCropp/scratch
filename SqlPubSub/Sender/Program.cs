using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Persistence.Sql;
using NServiceBus.Transport.SQLServer;

public static class Program
{

    public static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        Console.Title = "Samples.Sql.Sender";
        var endpointConfiguration = new EndpointConfiguration("Samples.Sql.Sender");
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.EnableInstallers();

        var connection = @"Data Source=.\SqlExpress;Database=NsbSamplesSql;Integrated Security=True;Max Pool Size=100";
        var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
        transport.ConnectionString(connection);
        transport.DefaultSchema("sender");
        transport.UseSchemaForQueue("error", "dbo");
        transport.UseSchemaForQueue("audit", "dbo");

        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.SqlVariant(SqlVariant.MsSqlServer);
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(connection);
            });
        persistence.Schema("sender");
        persistence.TablePrefix("");
        var subscriptionSettings = persistence.SubscriptionSettings();
        subscriptionSettings.CacheFor(TimeSpan.FromMilliseconds(1));

        SqlHelper.CreateSchema(connection, "sender");

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("Press any key to exit");

        var orderSubmitted = new OrderSubmitted
        {
            OrderId = Guid.NewGuid(),
        };
        await endpointInstance.Publish(orderSubmitted)
            .ConfigureAwait(false);
        var key = Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}