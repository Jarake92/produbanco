using System.ComponentModel.DataAnnotations;
using shared.comun.proxy.constantes;

namespace shared.comun.proxy.modelo;

public class Configuraciones
{
    [Required]
    public string Nombre { get; set; } = string.Empty;
    [Required]
    public string Url { get; set; } = string.Empty;
    [Required]
    public string Path { get; set; } = string.Empty;

    public int Timeout { get; set; } = ProxyConstantes.General.TimeOutGenerico;

    public static Configuraciones DefaultInstance => new();
}