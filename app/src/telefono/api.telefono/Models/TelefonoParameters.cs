using shared.comun.hetoas;

namespace api.telefono.Models;

public class TelefonoParameters : QueryStringParameters
{
    public TelefonoParameters() => OrderBy = "Numero";

    internal static TelefonoParameters Default(string id)
    {
        return new TelefonoParameters
        {
            Filter = $"Id == \"{Guid.Parse(id)}\"",
            Fields = "Id,IdCliente,Numero,Tipo,Operadora"
        };
    }

    internal static TelefonoParameters ByIdCliente(string id)
    {
        return new TelefonoParameters()
        {
            Filter = $"IdCliente == \"{id}\"",
            OrderBy = string.Empty,
            Fields = "Id,IdCliente,Numero,Tipo,Operadora"
        };
    }
}