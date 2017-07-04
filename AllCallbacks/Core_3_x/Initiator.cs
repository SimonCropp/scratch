using System.Threading.Tasks;
using NServiceBus;

public static class Initiator
{

    public static void Initiate(this IBus bus)
    {
        Parallel.ForEach(EndpointNames.All, endpointName =>
        {
            var remoteName = endpointName;

            bus.Send(remoteName, new IntMessage())
                .Register<int>(i =>
                {
                    Asserter.IsTrue(5 == i, "Incorrect int value");
                    Verifier.IntReplyReceivedFrom.Add(remoteName);
                });

            bus.Send(remoteName, new EnumMessage())
                .Register<CustomEnum>(i =>
                {
                    Asserter.IsTrue(CustomEnum.Value2 == i, "Incorrect enum value");
                    Verifier.EnumReplyReceivedFrom.Add(remoteName);
                });

            bus.Send(remoteName, new ObjectMessage())
                .Register(i =>
                {
                    var localResult = (CompletionResult)i.AsyncState;
                    var response = (ObjectResponseMessage)localResult.Messages[0];
                    Asserter.IsTrue("PropertyValue" == response.Property, "Incorrect object value");
                    Verifier.ObjectReplyReceivedFrom.Add(remoteName);
                }, null);
        });
    }
}