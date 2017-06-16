using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

public class RemoteHandler :
    IHandleMessages<RemoteMessage>
{
    static ILog log = LogManager.GetLogger<RemoteHandler>();

    public Task Handle(RemoteMessage message, IMessageHandlerContext context)
    {
        log.Info($"Hello from RemoteHandler: {message.SendFromContext}");
        return Task.CompletedTask;
    }
}