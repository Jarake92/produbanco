using api.cliente.Contracts;
using api.cliente.Models;
using shared.comun.hetoas;
using shared.comun.hetoas.extensions;
using System.Linq.Dynamic.Core;

namespace api.cliente.Services;

public class ClienteHelper : EntityHelper<Cliente>, IClienteHelper
{
    public ClienteHelper(
        ISortHelper<Cliente> sortHelper,
        IDataShaper<Cliente> dataShaper) : base(sortHelper, dataShaper)
    {
    }

    public override IEnumerable<Cliente> GetFilteredEntities(
        IQueryable<Cliente> entities,
        QueryStringParameters entityParameters)
    {
        if (!entities.Any() || string.IsNullOrWhiteSpace(entityParameters.Filter))
            return entities;

        if (!entityParameters.Filter.Contains("=="))
            return entities.Where(x => x.LastName.Contains(entityParameters.Filter));

        return entities.Where(entityParameters.Filter).ToList();
    }
}