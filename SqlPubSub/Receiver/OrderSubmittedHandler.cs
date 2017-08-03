using System;
using System.Threading.Tasks;
using NServiceBus;

public class OrderSubmittedHandler :
    IHandleMessages<OrderSubmitted>
{
    public Task Handle(OrderSubmitted message, IMessageHandlerContext context)
    {
        Console.WriteLine("Got event");
        return Task.CompletedTask;
    }
}