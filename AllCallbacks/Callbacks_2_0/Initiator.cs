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
                var intResult = bus.Request<int>(new IntMessage(), GetSendOptions(remoteName)).Result;
                Asserter.IsTrue(5 == intResult, "Incorrect int value");
                Verifier.IntReplyReceivedFrom.Add(remoteName);

                var enumResult = bus.Request<CustomEnum>(new EnumMessage(), GetSendOptions(remoteName)).Result;
                Asserter.IsTrue(CustomEnum.Value2 == enumResult, "Incorrect enum value");
                Verifier.EnumReplyReceivedFrom.Add(remoteName);

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