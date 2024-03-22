# Autenticación

Implementación de autenticación y autorización utilizando JWT Token. Para ello se ha creado la
clase [OpenIddictConfiguration.cs](../src/comun/Authentication/OpenIddictConfiguration.cs) donde se especifica el URL
del servidor de autenticación, la audiencia y la llave de encriptación.

En el método de extensión `AddOpenIddictAuthentication` en la
clase [AuthenticationExtensions.cs](../src/comun/Authentication/Extensiones/AuthenticationExtensions.cs) se registran
los servicios necesarios para la autenticación. Siempre es necesario utilizar los middlewares del framework de
autenticación de ASP.NET Core, en este caso se utiliza el middleware `UseAuthentication` para habilitar la autenticación
en la aplicación. También se utiliza el middleware `UseAuthorization` para habilitar la autorización en la aplicación.

```csharp
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
```

Para el funcionamiento de la autenticación es necesaria la siguiente configuración en el archivo `appsettings.json`:

```json
{
  "OpenIddictConfiguration": {
    "Authority": "https://localhost:7200",
    "Audience": "api",
    "EncryptionKey": "Sj/N7WndCnFm8nq[UBArc6_L}yHcVD3b"
  }
}
```

En este ejemplo se utiliza el servicio de identidad del proyecto `GenesysBFF.IVR.Identidad`. Para mayor información
sobre el funcionamiento de este servicio, consultar la documentación del mismo.