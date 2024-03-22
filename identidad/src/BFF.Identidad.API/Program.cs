using BFF.Identidad.API;
using BFF.Identidad.API.Extensiones;
using BFF.Identidad.Aplicacion.Extensiones;
using BFF.Identidad.Dominio.Models;
using Microsoft.EntityFrameworkCore;
//using Produnet.BFF.Middleware.Extensiones;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var serilogConfiguration = LeerConfigExtensions.CargarConfig("serilog.json");
builder.Host.UseSerilog((_, services, configuration) => { configuration.UsarSerilog(serilogConfiguration, services); });
var config = builder.Configuration.CargarSeccion<OpenIddictSettings>();

builder.Services.ConfigurarApplicationInsight();
builder.Services.RedireccionHttps();
builder.Services.ConfigurarOAUth2(config);
builder.Services.Autenticacion();
// Add services to the container.
builder.Services.AgregarServiciosAplicacion();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(DbContext));
    options.UseOpenIddict();
});

builder.Services.AddHostedService<CredencialesCliente>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();