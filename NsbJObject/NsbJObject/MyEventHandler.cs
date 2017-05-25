using System;
using System.Threading.Tasks;
using NServiceBus;

class MyEventHandler : IHandleMessages<MyEvent>
{
    public Task Handle(MyEvent message, IMessageHandlerContext context)
    {
        Console.Write("Hello from MyEventHandler. Changes " + message.Changes);
        return Task.FromResult(0);
    }
}