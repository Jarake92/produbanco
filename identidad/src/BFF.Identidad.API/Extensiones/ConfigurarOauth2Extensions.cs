using BFF.Identidad.Dominio.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BFF.Identidad.API.Extensiones
{
    public static class ConfigurarOauth2Extensions
    {
        public static void Autenticacion(this IServiceCollection services)
        {
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => { options.LoginPath = "/account/login"; });
        }

        public static void ConfigurarOAUth2(this IServiceCollection services, OpenIddictSettings config)
        {
            services
                .AddOpenIddict()
                .AddCore(options =>
                {
                    options
                        .UseEntityFrameworkCore()
                        .UseDbContext<DbContext>();
                })
                .AddServer(options =>
                {
                    options.AllowClientCredentialsFlow();
                    options.AllowAuthorizationCodeFlow()
                           .RequireProofKeyForCodeExchange();

                    options.SetIssuer(new Uri(config.Issuer));

                    options.SetAuthorizationEndpointUris("/connect/authorize");
                    options.SetTokenEndpointUris($"{config.Sitio}/connect/token");
                    options.SetCryptographyEndpointUris($"{config.Sitio}/.well-known/jwks");
                    options.SetConfigurationEndpointUris($"{config.Sitio}/.well-known/openid-configuration");

                    options.AddEncryptionKey(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.EncryptionKey)));
                    options.AddEphemeralSigningKey();
                    options.AddEphemeralEncryptionKey();
                    //options.DisableAccessTokenEncryption();

                    options.RegisterScopes("api");

                    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(double.Parse(config.AccessTokenLifetimeMinutos)));

                    options.UseAspNetCore()
                        .EnableTokenEndpointPassthrough()
                        .EnableAuthorizationEndpointPassthrough();
                });
        }
    }
}
