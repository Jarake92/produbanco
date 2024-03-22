using System.Reflection;
using api.cliente.Database;
using api.cliente.Extensions;
using HealthChecks.UI.Client;
using Microsoft.IdentityModel.Logging;
using Serilog;
using shared.comun.api_version.extensiones;
using shared.comun.api_version.modelo;
using shared.comun.Authentication;
using shared.comun.Authentication.Extensiones;
using shared.comun.configuracion;
using shared.comun.EntityFramework.Extensions;
using shared.comun.hearth.constantes;
using shared.comun.hearth.Extensiones;
using shared.comun.hearth.Modelo;
using shared.comun.service_discovery.consul.server;
using shared.comun.Telemetry;
using shared.comun.Telemetry.Extensiones;
using HealthCheckOptions = Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

IdentityModelEventSource.ShowPII = true;

Serilog(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConsulRegistro(builder);

//****************************************
// Custom Configurations
//****************************************

Persistencia(builder);

ApiSalud(builder);

builder.Services.AddOpenIddictAuthentication(LoadConfiguracion<OpenIddictConfiguration>.Instancia[nameof(OpenIddictConfiguration)]);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();

//****************************************
// Custom Configurations
//****************************************
app.UseTelemetryMiddlewares(
    LoadConfiguracion<TelemetryMiddlewaresOptions>.Instancia[nameof(TelemetryMiddlewaresOptions)]);

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks(ApiHealthConstantes.EndPoint.LiveEndpoint, new HealthCheckOptions
    {
        Predicate = _ => true,
        AllowCachingResponses = false,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

static void Serilog(WebApplicationBuilder builder)
{
    builder.AddSerilog("serilog.json");
    builder.Services.AddTelemetry(LoadConfiguracion<TelemetryConfiguration>.Instancia[nameof(TelemetryConfiguration)]);
}

static void ConsulRegistro(WebApplicationBuilder builder)
{
    builder.Services.Configure<ConsulConfigModel>(builder.Configuration.GetSection(ConfiguracionSeccion.SeccionConsul));
    builder.Services.ConfigurarConsul(builder.Configuration);
}

static void ApiSalud(WebApplicationBuilder builder)
{
    var dbConnection = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.VersionMicroservicio(
    LoadConfiguracion<RootApiVersion>.Instancia[ConfiguracionSeccion.SeccionApiVersion]);

    builder.Services.AddCustomApiHealth(new ApiHealthSettings
    {
        NombreServicio = Assembly.GetExecutingAssembly().GetName().Name!,
        UrlBaseDatos = dbConnection!
    }, builder.Configuration.GetValue<bool>("UseInMemoryDatabase"));
}

static void Persistencia(WebApplicationBuilder builder)
{
    var dbConnection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.PersistenciaMemoria();
    builder.Services.AddCustomDbContext<ClientesDbContext>(builder.Configuration, dbConnection!);
}