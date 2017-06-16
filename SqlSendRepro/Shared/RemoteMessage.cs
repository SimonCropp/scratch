using NServiceBus;

public class RemoteMessage :
    IMessage
{
    public string SendFromContext { get; set; }
}