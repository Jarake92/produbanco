using System.Diagnostics;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using shared.comun.hearth.Modelo;
using System.Text;

namespace shared.comun.hearth.Logica;

public class ApiHealth : IHealthCheck
{
    private readonly StringBuilder _nombreServicio = new("Health Check Microservicio");

    public ApiHealth(string nombreServicio)
    {
        _nombreServicio.Append($" - {nombreServicio}");
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var respuesta = new Dictionary<string, object>();
        try
        {
            var objHealth = new ApiModelHealth
            {
                NombreServicio = _nombreServicio.ToString(),
                CpuUsage = await GetCpuUsage(),
                MemoryUsage = GetMemoryUsage()
            };
            respuesta.TryAdd("Informacion Micro", objHealth);
            return await Task.FromResult(HealthCheckResult.Healthy(data: respuesta));
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy($"Servicio: {_nombreServicio}", ex);
        }
    }

    private static async Task<double> GetCpuUsage()
    {
        var startTime = DateTime.UtcNow;
        var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
        await Task.Delay(500);

        var endTime = DateTime.UtcNow;
        var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
        var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
        var totalMsPassed = (endTime - startTime).TotalMilliseconds;
        var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
        return cpuUsageTotal * 100;
    }

    private static double GetMemoryUsage()
    {
        using var currentProcess = Process.GetCurrentProcess();
        return currentProcess.PrivateMemorySize64;
    }
}