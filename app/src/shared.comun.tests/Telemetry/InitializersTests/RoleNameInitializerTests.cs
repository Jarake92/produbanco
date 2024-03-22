using System.Collections.Concurrent;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using shared.comun.Telemetry.Initializers;

namespace shared.comun.tests.Telemetry.InitializersTests;

public class RoleNameInitializerTests
{
    private ConcurrentQueue<ITelemetry> TelemetryItems { get; } = new();

    [Fact]
    public void RoleNameShouldBeSetTest()
    {
        var telemetryClient = CreateMockTelemetryClient();

        var componentUnderTest = new RoleNameTestComponent(telemetryClient);

        componentUnderTest.Test();

        Assert.True(TelemetryItems.TryDequeue(out var first));
        Assert.IsType<RequestTelemetry>(first);
      
        Assert.Equal("TestRoleName", first?.Context.Cloud.RoleName);
        Assert.Equal("TestRoleName", first?.Context.Cloud.RoleInstance);
    }

    private TelemetryClient CreateMockTelemetryClient()
    {
        var telemetryConfiguration = new TelemetryConfiguration
        {
            ConnectionString = "InstrumentationKey=" + Guid.NewGuid(),
            TelemetryChannel = new StubTelemetryChannel(TelemetryItems.Enqueue)
        };

        // Telemetry initializer to test
        telemetryConfiguration.TelemetryInitializers.Add(new RoleNameTelemetryInitializer("TestRoleName"));

        return new TelemetryClient(telemetryConfiguration);
    }
}

public class RoleNameTestComponent
{
    private readonly TelemetryClient _telemetryClient;
    public RoleNameTestComponent(TelemetryClient telemetryClient) => _telemetryClient = telemetryClient;

    public void Test()
    {
        using var operation = _telemetryClient.StartOperation<RequestTelemetry>("RequestTelemetry");
    }
}