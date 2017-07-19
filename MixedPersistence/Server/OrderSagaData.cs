using System;
using NServiceBus;

public class OrderSagaData :
    ContainSagaData
{
    public virtual Guid OrderId { get; set; }
    public virtual string OrderDescription { get; set; }
}
