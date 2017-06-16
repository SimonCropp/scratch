using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

public class LocalHandler :
    IHandleMessages<LocalMessage>
{
    static ILog log = LogManager.GetLogger<LocalHandler>();

    public Task Handle(LocalMessage message, IMessageHandlerContext context)
    {
        log.Info("Sending to Receiver from MessageHandlerContext");

        var remoteMessage = new RemoteMessage
        {
            SendFromContext = "MessageHandlerContext"
        };
        return context.Send("SqlServerSendRepro.Receiver", remoteMessage);
    }
}