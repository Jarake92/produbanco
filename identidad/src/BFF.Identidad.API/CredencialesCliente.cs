using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace BFF.Identidad.API;

public class CredencialesCliente : IHostedService
{
    private readonly IConfiguration _iConfiguration;
    private readonly IServiceProvider _iServiceProvider;

    public CredencialesCliente(IConfiguration iConfiguration, IServiceProvider iServiceProvider)
    {
        _iConfiguration = iConfiguration;
        _iServiceProvider = iServiceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _iServiceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync(_iConfiguration["OpenIddictSettings:ClientId"],
                cancellationToken) is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = _iConfiguration["OpenIddictSettings:ClientId"],
                ClientSecret = _iConfiguration["OpenIddictSettings:ClientSecret"],
                DisplayName = "IVR",
                RedirectUris = { new Uri("https://oauth.pstmn.io/v1/callback") },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.Endpoints.Authorization,

                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,

                    OpenIddictConstants.Permissions.Prefixes.Scope + "api",

                    OpenIddictConstants.Permissions.ResponseTypes.Code
                }
            }, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}