using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace shared.comun.Authentication.Extensiones;

public static class AuthenticationExtensions
{
    public static void AddOpenIddictAuthentication(
        this IServiceCollection services,
        OpenIddictConfiguration configuration)
    {
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(authenticationScheme: JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = ObtenerInformacion(configuration, "Authority");
                options.Audience = ObtenerInformacion(configuration, "Audience");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ObtenerInformacion(configuration, "EncryptionKey") ?? string.Empty)),
                    ClockSkew = TimeSpan.FromSeconds(3),
                    ValidateIssuer = true,
                    ValidIssuer = "https://iam.arquitectura.com:4431/",
                    ValidAudience = "api",
                    SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    {
                        var jwt = new JsonWebToken(token); // here was JwtSecurityToken
                        if (parameters.ValidateIssuer && parameters.ValidIssuer != jwt.Issuer)
                            return null;
                        return jwt;
                    }
                };
            });
    }

    private static string? ObtenerInformacion(OpenIddictConfiguration configuration, string value)
    {
        return value switch
        {
            "Authority" => !string.IsNullOrEmpty(configuration.Authority) ? configuration.Authority : Environment.GetEnvironmentVariable(value.ToUpperInvariant()),
            "Audience" => !string.IsNullOrEmpty(configuration.Audience) ? configuration.Audience : Environment.GetEnvironmentVariable(value.ToUpperInvariant()),
            "EncryptionKey" => !string.IsNullOrEmpty(configuration.EncryptionKey) ? configuration.EncryptionKey : Environment.GetEnvironmentVariable(value.ToUpperInvariant()),
            _ => string.Empty
        };
    }
}