using System.Threading.Tasks;
using NServiceBus;

public class Handler :
    IHandleMessages<IntMessage>,
    IHandleMessages<EnumMessage>,
    IHandleMessages<ObjectMessage>
{
    public Task Handle(IntMessage intMessage, IMessageHandlerContext context)
    {
        return context.Reply(5);
    }

    public Task Handle(EnumMessage message, IMessageHandlerContext context)
    {
        return context.Reply(CustomEnum.Value2);
    }

    public Task Handle(ObjectMessage message, IMessageHandlerContext context)
    {
        var objectResponseMessage = new ObjectResponseMessage
        {
            Property = "PropertyValue"
        };
        return context.Reply(objectResponseMessage);
    }
}