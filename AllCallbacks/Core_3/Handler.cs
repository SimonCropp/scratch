using NServiceBus;

public class Handler :
    IHandleMessages<ObjectMessage>
{
    public IBus Bus { get; set; }


    public void Handle(ObjectMessage message)
    {
        Bus.Reply(new ObjectResponseMessage
        {
            Property = "PropertyValue"
        });
    }

}