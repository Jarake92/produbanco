# Telemetría

Esta carpeta contiene toda la configuración necesaria para el envío de telemetría a Application Insights además de la configuración de Serilog.

## Configuración de Serilog

El metodo `AddSerilog` de la clase [TelemetryExtensions.cs](../src/comun/Telemetry/Extensiones/TelemetryExtensions.cs)
registra el servicio de telemetría en el contenedor de dependencias de la aplicación. Recibe como parámetro el archivo
de configuración de Serilog en formato JSON.

Cada API cuenta con un archivo de configuración de Serilog llamado serilog.json, el cual especifica los parámetros de
configuración de Serilog. Este archivo primero declara los sinks a utilizar:

```json
{
  "Using": [
    "Serilog.Expressions",
    "Serilog.Sinks.Console",
    "Serilog.Sinks.ApplicationInsights"
  ]
}
```

Después se especifican los niveles mínimos de log para cada sink. Por defecto el nivel mínimo es `Warning` para
minimizar la cantidad de información que se envía a Application Insights. Se puede bajar a `Information` para obtener
más información de los logs.

```json
{
  "MinimumLevel": {
    "Default": "Warning",
    "Override": {
      "Microsoft": "Warning",
      "System": "Warning",
      "AspNetCore.HealthChecks.UI": "Warning",
      "HealthChecks": "Warning"
    }
  }
}
```

Seguido de la configuración específica de cada sink. En el caso de Application Insights, se especifica el string de
conexión y el tipo de conversor de telemetría

```json
{
  "WriteTo": [
    {
      "Name": "Console"
    },
    {
      "Name": "ApplicationInsights",
      "Args": {
        "connectionString": "CONNECTION_STRING",
        "telemetryConverter": "Serilog.Sinks.ApplicationInsights.TelemetryConverters.TraceTelemetryConverter, Serilog.Sinks.ApplicationInsights"
      }
    }
  ]
}
```

Y por último se especifica los filtros de enriquecimiento y los enriquecedores de telemetría. En este ejemplo se excluye
de los logs las peticiones al endpoint de healthchecks:

```json
{
  "Filter": [
    {
      "Name": "ByExcluding",
      "Args": {
        "expression": "EndsWith(RequestPath, '/healthz') and StatusCode = 200"
      }
    }
  ],
  "Enrich": [
    "FromLogContext"
  ]
}
```

También es necesario llamar al middleware `UseSerilogRequestLogging` de Serilog en el pipeline de la aplicación. Este
middleware se encarga de enriquecer los logs con información de la petición HTTP y de la aplicación, y es llamado dentro
del método `UseTelemetryMiddlewares` de la
clase [MiddlewareExtensions.cs](../src/comun/Telemetry/Extensiones/MiddlewareExtensions.cs).

### RequestEnricher.EnrichFromRequest

El enriquecedor `RequestEnricher.EnrichFromRequest` se encarga de enriquecer los logs con información de la petición
HTTP.

### RequestEnricher.CustomGetLevel

El enriquecedor `RequestEnricher.CustomGetLevel` se encarga de enriquecer los logs con el nivel de log que se debe
utilizar para cada petición HTTP. Básicamente, si el estado de la respuesta es 500 o superior, el nivel de log
es `Error`; si es 400 o menor a 500, el nivel de log es `Warning`; y si es 200 o menor a 400, el nivel de log
es `Information`.

## Log Levels de Serilog

- Verbose
- Debug
- Information
- Warning
- Error
- Fatal

## Configuración de Application Insights

Para configurar Application Insights, se debe especificar el string de conexión en el archivo de configuración de la aplicación:

```json
{
  "ApplicationInsights": {
    "InstrumentationKey": "INSTRUMENTATION_KEY"
  }
}
```

También se deben especificar las dos secciones necesarias para que Application Insights funcione correctamente. `TelemetryConfiguration` se utiliza para configurar el rol de la aplicación y que se debe loggear. `TelemetryMiddlewaresOptions` se utiliza para enriquecer o filtrar los logs de Application Insights.

```json
{
  "TelemetryConfiguration": {
    "RoleName": "Cliente",
    "LogSqlStatements": true,
    "LogSqlParameters": true,
    "LogStaticResources": false
  },
  "TelemetryMiddlewaresOptions": {
    "LogRequestHeaders": true,
    "LogRequestBody": true,
    "LogResponseBody": true,
    "MaxBodySizeInBytes": 4096,
    "LogExceptions": true
  }
}
```

### TelemetryConfiguration

Después, se debe registrar el servicio de Application Insights en el contenedor de dependencias de la aplicación. Esto se realiza llamando al método `AddTelemetry` de la clase [TelemetryExtensions.cs](../src/comun/Telemetry/Extensiones/TelemetryExtensions.cs). Aquí se llama al método `AddApplicationInsightsTelemetry` el cual es necesario para configurar Application Insights. Este método también registra los servicios especificados en la configuración de `TelemetryConfiguration` mostrada arriba.

#### Telemetry Initializers

Los `Telemetry Initializers` son clases que se encargan de agregar información a los eventos de Application Insights antes de que sean enviados. En este proyecto se han creado dos inicializadores:

- [RoleNameTelemetryInitializer.cs](../src/comun/Telemetry/Initializers/RoleNameTelemetryInitializer.cs)

Telemetry Initializer que se encarga de agregar el nombre del rol para poder visualizar el servicio en el Application Map de Application Insights.

- [SqlTelemetryInitializer.cs](../src/comun/Telemetry/Initializers/SqlTelemetryInitializer.cs)

Este inicializador se encarga de agregar los parámetros de las consultas SQL a los eventos de Application Insights, en caso de que el request sea una dependencia de SQL.

#### Telemetry Processors

Los `Telemetry Processors` son clases que se encargan de filtrar los eventos de Application Insights antes de que sean enviados. En este proyecto se ha creado un procesador:

- [SuppressStaticResourcesProcessor.cs](../src/comun/Telemetry/Processors/SuppressStaticResourcesProcessor.cs)

Este procesador se encarga de filtrar los eventos de Application Insights que corresponden a recursos estáticos, como ser archivos css, js, imágenes, etc.

**Nota: La diferencia entre inicializadores y procesadores es que los inicializadores se ejecutan siempre que sean registrados y los procesadores se ejecutan dependiendo del procesador ejecutado anteriormente.**

#### LogSqlStatements

El parámetro `LogSqlStatements` de `TelemetryConfiguration` se utiliza para loggear las consultas SQL que se realizan a la base de datos.

#### LogSqlParameters

El parámetro `LogSqlParameters` de `TelemetryConfiguration` se utiliza para loggear los parámetros de las consultas SQL que se realizan a la base de datos.

#### LogStaticResources

El parámetro `LogStaticResources` de `TelemetryConfiguration` se utiliza para loggear las peticiones a archivos estáticos como CSS, JS, etc.

### TelemetryMiddlewaresOptions

Los valores en la configuración de `TelemetryMiddlewaresOptions` simplemente indican si un middleware debe ejecutarse o no. Por ejemplo, si `LogRequestHeaders` es `true`, el middleware `RequestHeadersLoggingMiddleware` se encargará de enriquecer los logs con los headers de la petición HTTP.

#### Middlewares

- [RequestHeadersLoggingMiddleware.cs](../src/comun/Telemetry/Middlewares/RequestHeadersLoggingMiddleware.cs)

Middleware que se encarga de agregar los headers del request a la telemetría de Application Insights.

- [RequestBodyLoggingMiddleware.cs](../src/comun/Telemetry/Middlewares/RequestBodyLoggingMiddleware.cs)

Middleware que se encarga de agregar el body del request a la telemetría de Application Insights.

- [ResponseBodyLoggingMiddleware.cs](../src/comun/Telemetry/Middlewares/ResponseBodyLoggingMiddleware.cs)

Middleware que se encarga de agregar el body del response a la telemetría de Application Insights.

- [ExceptionLoggingMiddleware.cs](../src/comun/Telemetry/Middlewares/ExceptionLoggingMiddleware.cs)

Middleware que se encarga de agregar las excepciones a la telemetría de Application Insights.