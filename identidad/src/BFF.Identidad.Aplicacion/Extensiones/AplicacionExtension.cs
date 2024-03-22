using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BFF.Identidad.Aplicacion.Extensiones
{
    public static class AplicacionExtension
    {
        public static IServiceCollection AgregarServiciosAplicacion(this IServiceCollection iServiceCollection)
        {
            // MediatR
            iServiceCollection.AddMediatR(Assembly.GetExecutingAssembly());

            return iServiceCollection;
        }
    }
}
