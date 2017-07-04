using NServiceBus;

public class Handler :
    IHandleMessages<ObjectMessage>
{
    public IBus Bus { get; set; }

    public void Handle(ObjectMessage message)
    {
        var response = new ObjectResponseMessage
        {
            Property = "PropertyValue"
        };
        Bus.Reply(response);
    }
}