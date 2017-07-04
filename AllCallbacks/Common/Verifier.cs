using System.Collections.Concurrent;

public class Verifier
{

    public static void AssertExpectations()
    {
        foreach (var endpointName in EndpointNames.All)
        {
            IntReplyReceivedFrom.VerifyContains(endpointName, $"{EndpointNames.EndpointName} expected a int reply to be Received From {endpointName}");
            EnumReplyReceivedFrom.VerifyContains(endpointName, $"{EndpointNames.EndpointName} expected a enum reply to be Received From {endpointName}");
            ObjectReplyReceivedFrom.VerifyContains(endpointName, $"{EndpointNames.EndpointName} expected a object reply to be Received From {endpointName}");
        }
    }

    public static ConcurrentBag<string> IntReplyReceivedFrom = new ConcurrentBag<string>();
    public static ConcurrentBag<string> EnumReplyReceivedFrom = new ConcurrentBag<string>();
    public static ConcurrentBag<string> ObjectReplyReceivedFrom = new ConcurrentBag<string>();
}