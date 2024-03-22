namespace shared.comun.hearth.Modelo;

public record ApiModelHealth
{
    public string NombreServicio { get; set; } = string.Empty;
    public double CpuUsage { get; set; }
    public double MemoryUsage { get; set; }
}