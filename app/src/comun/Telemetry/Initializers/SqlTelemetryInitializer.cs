using System.Data.Common;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Data.SqlClient;

namespace shared.comun.Telemetry.Initializers;

public class SqlTelemetryInitializer :  ITelemetryInitializer
{
    public void Initialize(ITelemetry telemetry)
    {
        if (telemetry is not DependencyTelemetry supportedTelemetry) return;

        if (supportedTelemetry.Type != "SQL" ||
            !supportedTelemetry.TryGetOperationDetail("SqlCommand", out var command)) return;

        foreach (DbParameter parameter in ((SqlCommand)command).Parameters)
        {
            supportedTelemetry.Properties.Add(parameter.ParameterName, parameter.Value?.ToString());
        }
    }
}