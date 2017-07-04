using NServiceBus;

public class Handler :
    IHandleMessages<IntMessage>,
    IHandleMessages<EnumMessage>,
    IHandleMessages<ObjectMessage>
{
    public IBus Bus { get; set; }

    public void Handle(IntMessage intMessage)
    {
        Bus.Return(5);
    }

    public void Handle(EnumMessage message)
    {
        Bus.Return(CustomEnum.Value2);
    }

    public void Handle(ObjectMessage message)
    {
        Bus.Reply(new ObjectResponseMessage
        {
            Property = "PropertyValue"
        });
    }

}