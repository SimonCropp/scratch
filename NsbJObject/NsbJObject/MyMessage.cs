using Newtonsoft.Json.Linq;
using NServiceBus;

public class MyMessage : IMessage
{
    public JObject Changes { get; set; }
}