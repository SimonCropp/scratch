using NServiceBus;

[EndpointName("Samples.NServiceBus.Host")]
public class EndpointConfig :
    IConfigureThisEndpoint
{
    public void Customize(EndpointConfiguration endpointConfiguration)
    {
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.UsePersistence<NHibernatePersistence>();
        endpointConfiguration.UseTransport<LearningTransport>();
    }
}