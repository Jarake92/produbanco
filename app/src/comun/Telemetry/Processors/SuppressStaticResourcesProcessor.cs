using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace shared.comun.Telemetry.Processors;

public class SuppressStaticResourcesProcessor : ITelemetryProcessor
{
    private readonly ITelemetryProcessor _next;

    private static readonly List<string> Names = new()
        { "favicon.ico", "bootstrap", "jquery", "site.css", "site.js", "swagger", "styles.css" };

    public SuppressStaticResourcesProcessor(ITelemetryProcessor next)
    {
        _next = next;
    }

    public void Process(ITelemetry item)
    {
        if (IsOkToSend(item))
            _next.Process(item);
    }

    private static bool IsOkToSend(ITelemetry item)
    {
        if (item is RequestTelemetry requestTelemetry)
        {
            return !Names.Any(name => requestTelemetry.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        return true;
    }
}