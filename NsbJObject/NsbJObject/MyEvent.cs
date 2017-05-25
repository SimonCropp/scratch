using Newtonsoft.Json.Linq;
using NServiceBus;

public interface MyEvent : IEvent
{
    JObject Changes { get; set; }
}