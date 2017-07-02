using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NServiceBus;

public class MvcApplication :
    HttpApplication
{
    public static IEndpointInstance EndpointInstance;

    protected void Application_Start()
    {
        StartBus().GetAwaiter().GetResult();
        AreaRegistration.RegisterAllAreas();
        RouteTable.Routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            }
        );
    }

    protected void Application_End()
    {
        EndpointInstance?.Stop().GetAwaiter().GetResult();
    }

    async Task StartBus()
    {
        var endpointConfiguration = new EndpointConfiguration("Samples.Callbacks.WebSender");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        endpointConfiguration.UseTransport<MsmqTransport>();
        endpointConfiguration.SendFailedMessagesTo("error");

        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningMessagesAs(
            type =>
            {
                return type.Assembly.GetName().Name == "Shared";
            });
        endpointConfiguration.MakeInstanceUniquelyAddressable("1");
        endpointConfiguration.EnableCallbacks();

        EndpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
    }

}