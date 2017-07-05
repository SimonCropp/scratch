using System;

public static class MessageConventions
{
    public static bool IsMessage(Type type)
    {
        return type.Assembly.GetName().Name == "Common" &&
               type.Name.EndsWith("Message");
    }
}