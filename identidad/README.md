# Introduction

Servicio de autenticación utilizando OpenIddict

## Obtener token utilizando Postman y client credentials flow

- Abrir Postman
- Seleccionar el método POST
- En la URL, escribir la siguiente dirección: https://iam.arquitectura.com:4431/connect/token
- En el body, seleccionar la opción 'x-www-form-urlencoded'
- Agregar los siguientes valores:
- - grant_type: client_credentials
- - client_id: ae1f1830-a338-4447-8152-231df22fa6d0
- - client_secret: fmv/ndpqB)f@&y.K8+A2XHNWkj%f-p
- - scope: api
- Dar click en el botón 'Send'
- Se debe obtener un token de respuesta

![postman1.png](img%2Fpostman1.png)

## Obtener token utilizando Postman y authorization code flow

- Abrir Postman
- Crear un nuevo request
- Seleccionar la opción 'Authorization'
- Seleccionar el type 'OAuth 2.0'

![postman2.png](img%2Fpostman2.png)

- En la configuración 'Configure New Token', llenar los siguientes campos:
- - Token Name: Authorization Code
- - Grant Type: Authorization Code (With PKCE)
- - Callback URL: https://oauth.pstmn.io/v1/callback
- - Selecciónar la opción 'Authorize using browser'
- - Auth URL: https://iam.arquitectura.com:4431/connect/authorize
- - Access Token URL: https://iam.arquitectura.com:4431/connect/token
- - Client ID: ae1f1830-a338-4447-8152-231df22fa6d0
- - Client Secret: fmv/ndpqB)f@&y.K8+A2XHNWkj%f-p
- - Code Challenge Method: S256
- - Code Verifier: (dejar en blanco)
- - Scope: api
- - State: 1234
- - Client Authentication: Send client credentials in body

![postman3.png](img%2Fpostman3.png)

- Dar click en el botón 'Get New Access Token'
- Se debe abrir una ventana del navegador para autenticarse
- Ingresar el usuario y contraseña (cualquier usuario y contraseña)
- Después de autenticarse, se debe obtener un token de respuesta

## En caso de error al tratar de publicar:

Instalando el paquete `System.IdentityModel.Tokens.Jwt` puede dar más información sobre el error.

También, en el program.cs, se puede agregar el siguiente código para obtener más información:

```csharp
IdentityModelEventSource.ShowPII = true;
```

## En caso del siguiente error:

```shell
Unable to obtain configuration from: '[...].well-known/openid-configuration'
```

Verificar que el certificado sea válido y que el nombre del dominio sea el mismo que el que se está usando en el certificado.

## Creación de un certificado en IIS

- Abrir Powershell como administrador

```shell
New-SelfSignedCertificate -DnsName "localhost" -CertStoreLocation "cert:\LocalMachine\My"
```

Donde localhost es el nombre del dominio que se va a usar para el certificado

Para permitir que el certificado sea usado por IIS:

- Presionar la tecla de Windows
- Escribir 'cert'
- Seleccionar 'Manage Computer Certificates'
- Debajo de 'Personal' seleccionar 'Certificates'
- Buscar el certificado creado
- Dar click derecho sobre el certificado y seleccionar copiar
- Después, debajo de 'Trusted Root Certification Authorities' seleccionar 'Certificates' y dar click derecho y seleccionar pegar

Para utilizar el certificado en IIS:

- Abrir IIS
- Seleccionar el sitio web
- Dar click derecho y seleccionar 'Edit Bindings'
- Seleccionar el binding https
- Dejar la dirección IP en como 'All Unassigned'
- Definir el puerto que se va a usar
- En el host name, escribir el nombre del dominio que se definió en el certificado
- Seleccionar la opción 'Require Server Name Indication'
- Por último, seleccionar el certificado creado

## Referencias:

- [OpenIddict](https://dev.to/robinvanderknaap/setting-up-an-authorization-server-with-openiddict-part-i-introduction-4jid)
- [Crear certificado en IIS](https://moriyama.co.uk/about-us/news/blog-how-to-create-a-self-signed-certificate-for-your-local-iis-website/)

