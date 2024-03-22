using System.Reflection;
using api.direccion.Database;
using api.direccion.Extensions;
using HealthChecks.UI.Client;
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

builder.AddSerilog("serilog.json");
builder.Services.AddTelemetry(LoadConfiguracion<TelemetryConfiguration>.Instancia[nameof(TelemetryConfiguration)]);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ConsulConfigModel>(builder.Configuration.GetSection(ConfiguracionSeccion.SeccionConsul));
builder.Services.ConfigurarConsul(builder.Configuration);

//****************************************
// Custom Configurations
//****************************************
var dbConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.PersistenciaMemoria();
builder.Services.AddCustomDbContext<DireccionesDbContext>(builder.Configuration, dbConnection!);
builder.Services.VersionMicroservicio(
    LoadConfiguracion<RootApiVersion>.Instancia[ConfiguracionSeccion.SeccionApiVersion]);

builder.Services.AddCustomApiHealth(new ApiHealthSettings
{
    NombreServicio = Assembly.GetExecutingAssembly().GetName().Name!,
    UrlBaseDatos = dbConnection!
}, builder.Configuration.GetValue<bool>("UseInMemoryDatabase"));

builder.Services.AddOpenIddictAuthentication(LoadConfiguracion<OpenIddictConfiguration>.Instancia[nameof(OpenIddictConfiguration)]);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//****************************************
// Custom Configurations
//****************************************
app.UseTelemetryMiddlewares(LoadConfiguracion<TelemetryMiddlewaresOptions>.Instancia[nameof(TelemetryMiddlewaresOptions)]);

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