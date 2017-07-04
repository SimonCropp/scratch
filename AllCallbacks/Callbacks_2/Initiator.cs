using System.Threading.Tasks;
using NServiceBus;

public static class Initiator
{

    public static void Initiate(this IEndpointInstance bus)
    {
        foreach (var endpoint in EndpointNames.All)
        {
            var remoteName = endpoint;
            Task.Run(() =>
            {
                var response = bus.Request<ObjectResponseMessage>(new ObjectMessage(), GetSendOptions(remoteName)).Result;
                Asserter.IsTrue("PropertyValue" == response.Property, "Incorrect object value");
                Verifier.ObjectReplyReceivedFrom.Add(remoteName);
            });
        }
    }

    static SendOptions GetSendOptions(string remoteName)
    {
        var sendOptions = new SendOptions();
        sendOptions.SetDestination(remoteName);
        return sendOptions;
    }
}