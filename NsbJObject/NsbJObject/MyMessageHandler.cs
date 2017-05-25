using System;
using System.Threading.Tasks;
using NServiceBus;

class MyMessageHandler : IHandleMessages<MyMessage>
{
    public Task Handle(MyMessage message, IMessageHandlerContext context)
    {
        Console.WriteLine("Hello from MyMessageHandler. Changes " + message.Changes);
        return Task.FromResult(0);
    }
}