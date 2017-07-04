using NServiceBus;

public static class Initiator
{

    public static void Initiate(this IBus bus)
    {
        bus.Send("Core_3", new ObjectMessage())
            .Register(i =>
            {
                var localResult = (CompletionResult) i.AsyncState;
                var response = (ObjectResponseMessage) localResult.Messages[0];
            }, null);
        bus.Send("Core_4", new ObjectMessage())
            .Register(i =>
            {
                var localResult = (CompletionResult) i.AsyncState;
                var response = (ObjectResponseMessage) localResult.Messages[0];
            }, null);
        bus.Send("Callbacks_2", new ObjectMessage())
            .Register(i =>
            {
                var localResult = (CompletionResult) i.AsyncState;
                var response = (ObjectResponseMessage) localResult.Messages[0];
            }, null);
    }
}