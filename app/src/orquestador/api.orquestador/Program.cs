using System.Reflection;
using api.orquestador.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using shared.comun.api_version.extensiones;
using shared.comun.api_version.modelo;
using shared.comun.Authentication;
using shared.comun.Authentication.Extensiones;
using shared.comun.configuracion;
using shared.comun.hearth.constantes;
using shared.comun.hearth.Logica;
using shared.comun.proxy.modelo;
using shared.comun.Telemetry;
using shared.comun.Telemetry.Extensiones;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog("serilog.json");
builder.Services.AddTelemetry(LoadConfiguracion<TelemetryConfiguration>.Instancia[nameof(TelemetryConfiguration)]);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

//****************************************
// Custom Configurations
//****************************************
var rootObject = LoadConfiguracion<Rootobject>.Instancia[ConfiguracionSeccion.SeccionProxy];

builder.Services.Configure<Rootobject>(builder.Configuration.GetSection(ConfiguracionSeccion.SeccionProxy));

builder.Services.VersionMicroservicio(
    LoadConfiguracion<RootApiVersion>.Instancia[ConfiguracionSeccion.SeccionApiVersion]);
builder.Services.ServiciosProxy();
builder.Services.ConexionHttpClienteMicros(rootObject);

var assemblyName = Assembly.GetExecutingAssembly().GetName().Name!;

builder.Services
    .AddHealthChecksUI(settings =>
    {
        settings.SetHeaderText("Health Checks UI - Orquestador");
        settings.SetEvaluationTimeInSeconds(15);

        settings.AddHealthCheckEndpoint("Orquestador", "/healthz");
        if (rootObject?.ConfiguracionClient != null)
        {
            foreach (var configuracion in rootObject.ConfiguracionClient)
            {
                settings.AddHealthCheckEndpoint(configuracion.Nombre, $"{configuracion.Url}healthz");
            }
        }
    }).AddInMemoryStorage();
builder.Services
    .AddHealthChecks()
    .AddCheck(assemblyName, new ApiHealth(assemblyName))
    .AddAzureApplicationInsights(
        name: $"{assemblyName} - Application Insights",
        instrumentationKey: builder.Configuration.GetValue<string>("ApplicationInsights:InstrumentationKey")!,
        tags: new[] { "dependency" });

builder.Services.AddOpenIddictAuthentication(
    LoadConfiguracion<OpenIddictConfiguration>.Instancia[nameof(OpenIddictConfiguration)]);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

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
    endpoints.MapHealthChecksUI(setup =>
    {
        setup.ApiPath = ApiHealthConstantes.EndPoint.HealthApiEndpoint;
        setup.UIPath = ApiHealthConstantes.EndPoint.HealthUiEndpoint;
    });
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