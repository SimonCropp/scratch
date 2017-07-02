﻿using System;
using NServiceBus;
using NServiceBus.Installation.Environments;

class Program
{
    static void Main()
    {
        Console.Title = "Samples.FullDuplex.Server";
        Configure.Serialization.Json();
        var configure = Configure.With();
        configure.DefiningMessagesAs(
            type =>
            {
                return type.Assembly.GetName().Name == "Shared";
            });
        configure.Log4Net();
        configure.DefineEndpointName("Samples.FullDuplex.Server");
        configure.DefaultBuilder();
        configure.InMemorySagaPersister();
        configure.UseInMemoryTimeoutPersister();
        configure.InMemorySubscriptionStorage();
        configure.UseTransport<Msmq>();

        using (var startableBus = configure.UnicastBus().CreateBus())
        {
            var bus = startableBus
                .Start(() => configure.ForInstallationOn<Windows>().Install());
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}