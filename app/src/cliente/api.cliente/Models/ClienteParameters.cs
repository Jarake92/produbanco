using shared.comun.hetoas;

namespace api.cliente.Models;

public sealed class ClienteParameters : QueryStringParameters
{
    public ClienteParameters() => OrderBy = "Name";

    public static ClienteParameters Default(string id)
    {
        return new ClienteParameters
        {
            Filter = $"Id == \"{id}\"",
            OrderBy = string.Empty,
            Fields = "Id,Name,LastName,DateBirth"
        };
    }
}