### Demo

Messages v events. extra subscribers

##### Endpoint stopped

Doesn't effect user experience

##### Scale out/Competing consumer

##### Intermittent errors

eg network connectivity, db conflicts, outage due to deployment

##### error queue

* poison message
* bug in handler/saga




### [Sql Transport](https://docs.particular.net/transports/sql/)

#### Advantages:

 * known technology for most engineers
 * operations teams know how to maintain and support Sql
 * adequate performance

#### Disadvantages

* no first class queue management tool. for comparison in MSMQ queues can be managed and manipulated in [QueueExplorer](https://www.cogin.com/mq/), [Mqueue Viewer](https://www.mqueue.net/), native windows tooling, visual studio, etc. Most other transports also have equivalent tooling. With the Sql transport we are limited to SSMS and some limited support with ServiceInsight and ServicePulse.
* Arguably [using a Database As Queue is an anti-pattern](http://mikehadlow.blogspot.com/2012/04/database-as-queue-anti-pattern.html). However given our current low throughput we have not hit these limitations.

### [Sql Persistence](https://docs.particular.net/persistence/sql/)

#### Sql Scripts

On build the Sql Persistence produces all required Sql scripts for a given endpoint. These scripts are included as build assets and will be deployed with the app, and run at install time. Our [scripts are promoted](https://docs.particular.net/persistence/sql/controlling-script-generation#promotion) and committed to source control. This means changes to the generated scripts (eg new or changed sagas), can be visualised in the commit and tracked over time via the source control history

### Logging and diagnostics

#### [NServiceBus.Serilog](https://github.com/NServiceBusExtensions/NServiceBus.Serilog)

Forwards all NSB log messages to [Serilog](https://serilog.net/)

##### [Seq](https://getseq.net/)

Allows us to search, group and visualise log entries.

 * [Seq server](http://seq-dfc.cloud9.cabnet:5341/#/events)
 * [All exceptions](http://seq-dfc.cloud9.cabnet:5341/#/events?intersect=signal-m33303)
 * [Entries for an app](http://seq-dfc.cloud9.cabnet:5341/#/events?intersect=signal-322)

Currently considering using some [Seq Apps](https://docs.getseq.net/docs/installing-seq-apps) to forward errors (possibly sanitized) to Email, TFS, Slack, etc

#### Error Queue monitoring??

Currently manual. Need to automate. Considering [ServiceControl](https://docs.particular.net/servicecontrol/) or [AppDynamics](https://www.appdynamics.com/) or [SCOM](https://docs.microsoft.com/en-au/system-center/) via [Data Source Watcher](https://docs.microsoft.com/en-us/previous-versions/system-center/system-center-2012-R2/hh457575(v=sc.12)#watcher-nodes)

### [Message Attachments (SQL)](https://docs.particular.net/nservicebus/messaging/attachments-sql)

Implementation of the [Claim Check Pattern](https://www.enterpriseintegrationpatterns.com/patterns/messaging/StoreInLibrary.html)

<img src="https://www.enterpriseintegrationpatterns.com/img/StoreInLibrary.gif" style="height:200px"/>


Same pattern as the  [NSB DataBus](https://docs.particular.net/nservicebus/messaging/databus/) but several advantages:

 * Attachments are only retrieved on demand. So handlers that dont need an attachment do not incure the cost.
 * Support for streaming attachments. The databus uses only large byte arrays. Sql attachments allows streaming of attachments so only a small fraction of an attachment is ever in memory.
 * When used with the Sql Transport and Sql persistence, all data (messages, sagas, attachments) can all be written using the same Sql connection and transaction. This improves performance and provides a single atomic write with no need to escalate to DTC.

### Deduplication

To handle intermittent connectivity issues it is desirable to have a web client leverage a retry mechanism. So if a request fails, the same request can be immediately or periodically. To prevent this possibly resulting in duplicate message being placed on the queue, message deduplication has to occur.

### [SQL HTTP Passthrough](https://docs.particular.net/transports/sql/sql-http-passthrough)

Allows a message, defined as json body in a web page, to passthrough a web teir without the need to deserialize+serilize or to use the NSB pipeline. The json string is written directly to the SQL queue. Supports deduplication. Supports Attachments via a HTTP Miltipart form.

### [Fluent Message Validation](https://docs.particular.net/nservicebus/messaging/validation-fluentvalidation)

Messages can be validated both in the outgoing and incoming contexts. Outgoing is particularly helpful during developement dince it allows a subset of bugs to be caugh in the stack trace of the sender rather than in the stack trace of the receiver. This means stepping back through the stack takes you to the offending code, intead of needing to derive the sender. 

```
public class MyMessage : IMessage
{
    public string Content { get; set; }
}

public class MyMessageValidator : AbstractValidator<MyMessage>
{
    public MyMessageValidator()
    {
        RuleFor(_ => _.Content).NotEmpty();
    }
}
```

### Advice for people starting or considering NSB

 * Focus on primarily learning the messaging patterns and secondarily on specific tooling
 * Attend [Advanced Distributed Systems Design](https://particular.net/adsd) or take the [online course](https://learn-particular.thinkific.com/courses/adsd-online)

#### Amount of time/consideration when selecting technologies

In order from hard to easy

 1. Transport. It must be shared by all nodes in a system that want to communicate. It is difficult to migrate to different transport. A [Transport Bridge](https://docs.particular.net/nservicebus/bridge/) can help with this migration, or to help non homogeneous nodes communicate.
 2. Persistence. Difficult to migrate to a different technology. However easier than the transport since different nodes can use different persistence. So when migrating, nodes can be migrated in isolation.
 3. Serializer. Serialization is generally organisation wide, but NSB supports [multiple serializers running concurrently](https://docs.particular.net/nservicebus/serialization/#specifying-additional-deserializers) and there are [strategies for moving between them](https://docs.particular.net/samples/serializers/transitioning-formats/).   So it is relatively easy to switch between different serializers.
 4. logging, IOC.  Different nodes can use different technologies and both are easy to switch between different technologies