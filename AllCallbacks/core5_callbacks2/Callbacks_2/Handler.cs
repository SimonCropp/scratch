using System.Threading.Tasks;
using Messages;
using NServiceBus;

public class Handler :
    IHandleMessages<ObjectMessage>
{
    public Task Handle(ObjectMessage message, IMessageHandlerContext context)
    {
        var objectResponseMessage = new ObjectResponseMessage
        {
            Property = "PropertyValue"
        };
        return context.Reply(objectResponseMessage);
    }
}