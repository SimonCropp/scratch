using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Extensibility;
using NServiceBus.Features;
using NServiceBus.Persistence;
using NServiceBus.Unicast.Subscriptions;
using NServiceBus.Unicast.Subscriptions.MessageDrivenSubscriptions;

static class Program
{

    static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        Console.Title = "Samples.PubSub.Publisher";
        var endpointConfiguration = new EndpointConfiguration("Samples.PubSub.Publisher");

        endpointConfiguration.DisableFeature<TimeoutManager>();
        endpointConfiguration.UseTransport<MsmqTransport>();
        endpointConfiguration.DisableFeature<MessageDrivenSubscriptions>();
        endpointConfiguration.UsePersistence<CustomPersistence>();
        endpointConfiguration.UseSerialization<XmlSerializer>();
        endpointConfiguration.DisableFeature<AutoSubscribe>();
        endpointConfiguration.DisableFeature<Sagas>();
        endpointConfiguration.SendFailedMessagesTo(EndpointName.MsmqTransportConfigErrorQueue);
        endpointConfiguration.AuditProcessedMessagesTo(EndpointName.MsmqTransportConfigAuditQueue);

        endpointConfiguration.EnableInstallers();

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        await Start(endpointInstance)
            .ConfigureAwait(false);
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }

    static async Task Start(IEndpointInstance endpointInstance)
    {
        Console.WriteLine("Press '1' to publish the OrderReceived event");
        Console.WriteLine("Press any other key to exit");

        while (true)
        {
            var key = Console.ReadKey();
            Console.WriteLine();

            var orderReceivedId = Guid.NewGuid();
            if (key.Key == ConsoleKey.D1)
            {
                var orderReceived = new OrderReceived
                {
                    OrderId = orderReceivedId
                };
                await endpointInstance.Publish(orderReceived)
                    .ConfigureAwait(false);
                Console.WriteLine($"Published OrderReceived Event with Id {orderReceivedId}.");
            }
            else
            {
                return;
            }
        }

    }

    public class EndpointName
    {
        public const string MsmqTransportConfigInputQueue = "NServiceBus6.Test.Client.InputQueue";
        public const string MsmqTransportConfigAuditQueue = "NServiceBus6.Test.Client.AuditQueue";
        public const string MsmqTransportConfigErrorQueue = "NServiceBus6.Test.Client.ErrorQueue";
        public const string MsmqTransportConfigDestinationInputQueue = "NSBus.Message.Host.InputQueue";
    }

    public class CustomPersistence : PersistenceDefinition
    {
        internal CustomPersistence()
        {
            Supports<StorageType.Subscriptions>(s => s.EnableFeatureByDefault<CustomFeature>());
        }
    }

    public class CustomFeature : Feature
    {
        internal CustomFeature()
        {
        }

        protected override void Setup(FeatureConfigurationContext context)
        {
            var myProperty = context.Settings.GetOrDefault<string>("CustomSubscriptionStorage.MyProperty");
            context.Container.ConfigureComponent(b => new CustomSubscriptionStorage { MyProperty = myProperty }, DependencyLifecycle.SingleInstance);
        }
    }

    public class CustomSubscriptionStorage : ISubscriptionStorage
    {
        public string MyProperty { get; set; }

        public Task Subscribe(Subscriber subscriber, MessageType messageType, ContextBag context)
        {
            return Task.FromResult(0);
        }

        public Task Unsubscribe(Subscriber subscriber, MessageType messageType, ContextBag context)
        {
            return Task.FromResult(0);
        }

        public Task<IEnumerable<Subscriber>> GetSubscriberAddressesForMessage(IEnumerable<MessageType> messageTypes, ContextBag context)
        {
            var result = new HashSet<Subscriber> { new Subscriber(EndpointName.MsmqTransportConfigDestinationInputQueue, null) };
            return Task.FromResult((IEnumerable<Subscriber>)result);
        }
    }
}
