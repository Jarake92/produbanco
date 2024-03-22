namespace shared.comun.Telemetry;

public class TelemetryMiddlewaresOptions
{
    public bool LogRequestBody { get; set; }
    public bool LogResponseBody { get; set; }
    public int MaxBodySizeInBytes { get; set; }
    public bool LogRequestHeaders { get; set; }
    public bool LogExceptions { get; set; }
}