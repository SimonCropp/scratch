using System;
using System.Threading.Tasks;
using NServiceBus;

public class MessageSender :
    IWantToRunWhenEndpointStartsAndStops
{
    public Task Start(IMessageSession session)
    {
        var orderId = Guid.NewGuid();
        var startOrder = new StartOrder
        {
            OrderId = orderId
        };
        return session.SendLocal(startOrder);
    }

    public Task Stop(IMessageSession session)
    {
        return Task.CompletedTask;
    }
}