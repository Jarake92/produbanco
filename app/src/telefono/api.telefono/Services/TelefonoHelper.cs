using System.Linq.Dynamic.Core;
using api.telefono.Contracts;
using api.telefono.Models;
using shared.comun.hetoas;
using shared.comun.hetoas.extensions;

namespace api.telefono.Services;

public class TelefonoHelper : EntityHelper<Telefono>, ITelefonoHelper
{
    public TelefonoHelper(
        ISortHelper<Telefono> sortHelper,
        IDataShaper<Telefono> dataShaper) : base(sortHelper, dataShaper)
    {
    }

    public override IEnumerable<Telefono> GetFilteredEntities(
        IQueryable<Telefono> entities,
        QueryStringParameters entityParameters)
    {
        if (!entities.Any() || string.IsNullOrWhiteSpace(entityParameters.Filter))
            return entities;

        if (entityParameters.Filter.Contains("=="))
        {
            var tempInfo = entities.Where(entityParameters.Filter);
            return tempInfo.ToList();
        }

        if (entityParameters.Filter.Contains("like"))
        {
            var campo = entityParameters.Filter.Split("like", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            return campo[0] switch
            {
                "Numero" => entities.Where(x => x.Numero.Contains(campo[1])),
                "Operadora" => entities.Where(x => x.Operadora == ToEnum<Operadora>(campo[1])),
                "Tipo" => entities.Where(x => x.Tipo == ToEnum<TipoTelefono>(campo[1])),
                _ => throw new NotImplementedException(),
            };
        }

        return entities;
    }

    private static T ToEnum<T>(string texto) where T : struct
    {
        Enum.TryParse(texto, true, out T valor);

        return valor;
    }
}