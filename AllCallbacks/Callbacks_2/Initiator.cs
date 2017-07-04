using NServiceBus;

public static class Initiator
{

    public static void Initiate(this IEndpointInstance bus)
    {
        var response3 = bus.Request<ObjectResponseMessage>(new ObjectMessage(), GetSendOptions("Core_3")).Result;

        var response4 = bus.Request<ObjectResponseMessage>(new ObjectMessage(), GetSendOptions("Core_4")).Result;

        var response5 = bus.Request<ObjectResponseMessage>(new ObjectMessage(), GetSendOptions("Core_5")).Result;
    }

    static SendOptions GetSendOptions(string remoteName)
    {
        var sendOptions = new SendOptions();
        sendOptions.SetDestination(remoteName);
        return sendOptions;
    }
}