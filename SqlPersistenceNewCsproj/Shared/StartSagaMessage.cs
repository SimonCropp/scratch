using System;
using NServiceBus;

public class StartSagaMessage : IMessage
{
    public Guid MySagaId { get; set; }
}