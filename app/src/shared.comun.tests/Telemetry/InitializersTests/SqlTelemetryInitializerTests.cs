using System.Collections.Concurrent;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Data.SqlClient;
using shared.comun.Telemetry.Initializers;

namespace shared.comun.tests.Telemetry.InitializersTests;

public class SqlTelemetryInitializerTests
{
    private ConcurrentQueue<ITelemetry> TelemetryItems { get; } = new();

    [Fact]
    public void SqlParametersShouldBeSet()
    {
        var telemetryClient = CreateMockTelemetryClient();

        var componentUnderTest = new SqlTelemetryTestComponent(telemetryClient);

        componentUnderTest.Test();

        Assert.True(TelemetryItems.TryDequeue(out var first));
        Assert.True(((DependencyTelemetry)first).Properties.ContainsKey("param1"));
        Assert.True(((DependencyTelemetry)first).Properties.ContainsKey("param2"));
    }

    private TelemetryClient CreateMockTelemetryClient()
    {
        var telemetryConfiguration = new TelemetryConfiguration
        {
            ConnectionString = "InstrumentationKey=" + Guid.NewGuid(),
            TelemetryChannel = new StubTelemetryChannel(TelemetryItems.Enqueue)
        };

        // Telemetry initializer to test
        telemetryConfiguration.TelemetryInitializers.Add(new SqlTelemetryInitializer());

        return new TelemetryClient(telemetryConfiguration);
    }
}

internal class SqlTelemetryTestComponent
{
    private readonly TelemetryClient _telemetryClient;
    public SqlTelemetryTestComponent(TelemetryClient telemetryClient) => _telemetryClient = telemetryClient;

    public void Test()
    {
        var data = new SqlCommand("Test");
        data.Parameters.Add(new SqlParameter("param1", 1));
        data.Parameters.Add(new SqlParameter("param2", 2));

        var sqlDependency = new DependencyTelemetry("SQL", "Test", "Test", "Test", DateTimeOffset.Now,
            TimeSpan.FromSeconds(1), "200", true);
        sqlDependency.Context.StoreRawObject("SqlCommand", data);

       
        _telemetryClient.TrackDependency(sqlDependency);
    }
}