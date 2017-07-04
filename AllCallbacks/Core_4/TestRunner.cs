using System;
using System.Threading;
using NServiceBus;

public static class TestRunner
{

    public static void RunTests(IBus bus)
    {
        Thread.Sleep(TimeSpan.FromSeconds(10));
        bus.Initiate();

        Thread.Sleep(TimeSpan.FromSeconds(10));
        var disposable = bus as IDisposable;
        disposable?.Dispose();
        Verifier.AssertExpectations();
    }
}