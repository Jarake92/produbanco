using System.Linq.Dynamic.Core;
using api.direccion.Contracts;
using api.direccion.Models;
using shared.comun.hetoas;
using shared.comun.hetoas.extensions;

namespace api.direccion.Services
{
    public class DireccionHelper : EntityHelper<Direccion>, IDireccionHelper
    {
        public DireccionHelper(
            IDataShaper<Direccion> dataShaper,
            ISortHelper<Direccion> sortHelper) : base(sortHelper, dataShaper)
        {
        }

        public override IEnumerable<Direccion> GetFilteredEntities(
            IQueryable<Direccion> direcciones,
            QueryStringParameters parameters)
        {
            if (!direcciones.Any() || string.IsNullOrWhiteSpace(parameters.Filter))
                return direcciones;

            if (parameters.Filter.Contains("=="))
            {
                var tempInfo = direcciones.Where(parameters.Filter);
                return tempInfo.ToList();
            }

            if (parameters.Filter.Contains("like"))
            {
                var campo = parameters.Filter.Split("like", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                return campo[0] switch
                {
                    "Canton" => direcciones.Where(x => x.Canton.Contains(campo[1].Trim())),
                    "CallePrincipal" => direcciones.Where(x => x.CallePrincipal.Contains(campo[1])),
                    _ => direcciones.Where(x => x.Provincia.Contains(campo[1])),
                };
            }

            return direcciones.Where(x => x.Provincia.Contains(parameters.Filter));
        }
    }
}