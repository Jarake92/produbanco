using System.Net;

namespace BFF.Identidad.API.Extensiones
{
    public static class ConfigurarHttps
    {
        public static void RedireccionHttps(this IServiceCollection services)
        {
            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 4431;
                options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
            });
        }
    }
}
