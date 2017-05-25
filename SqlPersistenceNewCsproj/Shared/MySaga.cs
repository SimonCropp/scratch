using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Persistence.Sql;

public class MySaga : SqlSaga<MySaga.SagaData>,
    IAmStartedByMessages<StartSagaMessage>
{
    static ILog logger = LogManager.GetLogger(typeof(MySaga));

    protected override string CorrelationPropertyName => nameof(SagaData.MySagaId);

    protected override void ConfigureMapping(IMessagePropertyMapper mapper)
    {
        mapper.ConfigureMapping<StartSagaMessage>(_ => _.MySagaId);
    }

    public Task Handle(StartSagaMessage message, IMessageHandlerContext context)
    {
        Data.MySagaId = message.MySagaId;
        return Task.FromResult(0);
    }

    public class SagaData : ContainSagaData
    {
        public Guid MySagaId { get; set; }
    }

}