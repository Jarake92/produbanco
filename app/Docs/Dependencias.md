# Lista de dependencias de la aplicación

Aquí se listan las dependencias de la aplicación y una breve descripción de para qué se utilizan. Para ver más detalles,
como ser la versión de cada dependencia, se puede consultar el archivo `*.csproj` de cada proyecto o utilizar el
comando `dotnet list package`.

```shell
dotnet list state.orquestation.net6.sln package
```

## Proyectos: `api.cliente`, `api.direccion` y `api.telefono`

- AspNetCore.HealthChecks.UI.Client (Utilizada para el endpoint de healthcheck)
- Bogus (Utilizada para generar datos de prueba)
- Microsoft.ApplicationInsights.AspNetCore (Utilizada para enviar métricas a Application Insights)
- Microsoft.EntityFrameworkCore (Utilizada para acceder a la base de datos)
- Microsoft.EntityFrameworkCore.Design (Utilizada para acceder a la base de datos)
- Microsoft.EntityFrameworkCore.SqlServer (Utilizada para acceder a la base de datos)
- Serilog.Expressions (Utilizada para filtrar logs)
- Serilog.Sinks.ApplicationInsights (Utilizada para enviar logs a Application Insights)
- Serilog.Sinks.Console (Utilizada para enviar logs a la consola)
- Swashbuckle.AspNetCore (Utilizada para generar la documentación de la API)

## Proyecto: `api.orquestador`

- AspNetCore.HealthChecks.UI (Utilizada para el endpoint de healthcheck)
- AspNetCore.HealthChecks.UI.Client (Utilizada para el endpoint de healthcheck)
- AspNetCore.HealthChecks.UI.InMemory.Storage (Utilizada para el endpoint de healthcheck)
- Microsoft.ApplicationInsights.AspNetCore (Utilizada para enviar métricas a Application Insights)
- Newtonsoft.Json (Utilizada para serializar y deserializar objetos)
- Serilog.Expressions (Utilizada para filtrar logs)
- Serilog.Sinks.ApplicationInsights (Utilizada para enviar logs a Application Insights)
- Serilog.Sinks.Console (Utilizada para enviar logs a la consola)
- stateless (Utilizada para crear máquinas de estado)
- Swashbuckle.AspNetCore (Utilizada para generar la documentación de la API)
- System.Net.Http.Json (Utilizada para enviar peticiones HTTP)

## Proyecto: `shared.comun`

- AspNetCore.HealthChecks.AzureApplicationInsights (Utilizada para el endpoint de healthcheck)
- AspNetCore.HealthChecks.SqlServer (Utilizada para el endpoint de healthcheck)
- Consul (Utilizada para acceder a Consul)
- HealthChecks.Extensions (Utilizada para el endpoint de healthcheck)
- Microsoft.ApplicationInsights.AspNetCore (Utilizada para enviar métricas a Application Insights)
- Microsoft.AspNetCore.Authentication.JwtBearer (Utilizada para autenticar peticiones HTTP)
- Microsoft.AspNetCore.Authentication.OpenIdConnect (Utilizada para autenticar peticiones HTTP)
- Microsoft.AspNetCore.Http.Abstractions (Utilizada para acceder a la información de las peticiones HTTP)
- Microsoft.AspNetCore.Mvc.Versioning (Utilizada para versionar la API)
- Microsoft.Bcl.AsyncInterfaces (Utilizada para acceder a la información de las peticiones HTTP)
- Microsoft.EntityFrameworkCore (Utilizada para acceder a la base de datos)
- Microsoft.EntityFrameworkCore.InMemory (Utilizada para acceder a la base de datos)
- Microsoft.EntityFrameworkCore.SqlServer (Utilizada para acceder a la base de datos)
- Microsoft.Extensions.Configuration (Utilizada para acceder a la configuración de la aplicación)
- Microsoft.Extensions.Configuration.Abstractions (Utilizada para acceder a la configuración de la aplicación)
- Microsoft.Extensions.Configuration.Binder (Utilizada para acceder a la configuración de la aplicación)
- Microsoft.Extensions.Configuration.Json (Utilizada para acceder a la configuración de la aplicación)
- Microsoft.Extensions.DependencyInjection (Utilizada para acceder a la configuración de la aplicación)
- Microsoft.Extensions.Diagnostics.HealthChecks (Utilizada para el endpoint de healthcheck)
- Polly (Utilizada para implementar políticas de reintentos)
- Serilog.AspNetCore (Utilizada para enviar logs a la consola)
- System.Linq.Dynamic.Core (Utilizada para filtrar datos)