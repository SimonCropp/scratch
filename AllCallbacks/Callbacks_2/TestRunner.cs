using System;
using System.Threading.Tasks;
using NServiceBus;

public static class TestRunner
{
    public static async Task RunTests(IEndpointInstance bus)
    {
        await Task.Delay(TimeSpan.FromSeconds(10)).ConfigureAwait(false);
        bus.Initiate();

        await Task.Delay(TimeSpan.FromSeconds(10)).ConfigureAwait(false);
        await bus.Stop().ConfigureAwait(false);
        Verifier.AssertExpectations();
    }
}