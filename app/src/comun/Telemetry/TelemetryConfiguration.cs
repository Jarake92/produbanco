namespace shared.comun.Telemetry;

public class TelemetryConfiguration
{
    public string RoleName { get; set; } = null!;
    public bool LogSqlStatements { get; set; }
    public bool LogSqlParameters { get; set; }
    public bool LogStaticResources { get; set; }
}