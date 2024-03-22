using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace BFF.Identidad.API.Initializers;

public class RoleNameTelemetryInitializer : ITelemetryInitializer
{
    private readonly string _roleName;

    public RoleNameTelemetryInitializer(string roleName)
    {
        _roleName = roleName;
    }

    public void Initialize(ITelemetry telemetry)
    {
        if (telemetry.Context.Cloud.RoleName is not null) return;

        telemetry.Context.Cloud.RoleName = _roleName;
        telemetry.Context.Cloud.RoleInstance = _roleName;
    }
}