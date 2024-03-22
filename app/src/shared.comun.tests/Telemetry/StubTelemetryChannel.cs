using Microsoft.ApplicationInsights.Channel;

namespace shared.comun.tests.Telemetry;

internal sealed class StubTelemetryChannel : ITelemetryChannel
{
    private readonly Action<ITelemetry> _onSend;
    public bool? DeveloperMode { get; set; }
    public string EndpointAddress { get; set; } = "";

    public StubTelemetryChannel(Action<ITelemetry> onSend)
    {
        _onSend = onSend ?? throw new ArgumentNullException(nameof(onSend));
    }

    public void Send(ITelemetry item) => _onSend(item);

    public void Flush()
    {
    }

    public void Dispose()
    {
    }
}