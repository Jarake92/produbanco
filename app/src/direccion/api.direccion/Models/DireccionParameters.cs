using shared.comun.hetoas;

namespace api.direccion.Models;

public sealed class DireccionParameters : QueryStringParameters
{
    public DireccionParameters() => OrderBy = "Provincia";

    internal static DireccionParameters Default(string id)
    {
        return new DireccionParameters
        {
            Filter = $"Id == \"{Guid.Parse(id)}\"",
            Fields = "Id,IdCliente,Provincia,Canton,CallePrincipal"
        };
    }

    internal static DireccionParameters ByIdCliente(string id)
    {
        return new DireccionParameters()
        {
            Filter = $"IdCliente == \"{id}\"",
            OrderBy = string.Empty,
            Fields = "Id,IdCliente,Provincia,Canton,CallePrincipal"
        };
    }
}