FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.19 AS base
WORKDIR /app
EXPOSE 4431

ENV ASPNETCORE_URLS=https://+:4431

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN apk add nano --no-cache
RUN apk add --no-cache openssl
RUN apk add --no-cache curl
RUN apk add busybox-extras
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser


#SDK para la compilacion
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.19 AS build

#================================
#INFORMACION DE CAPAS
#================================
WORKDIR /src

COPY ["BFF.Identidad.Dominio/BFF.Identidad.Dominio.csproj", "BFF.Identidad.Dominio/"]
#restore para las dependencias
RUN dotnet restore "BFF.Identidad.Dominio/BFF.Identidad.Dominio.csproj"

COPY ["BFF.Identidad.Infraestructura/BFF.Identidad.Infraestructura.csproj", "BFF.Identidad.Infraestructura/"]
#restore para las dependencias
RUN dotnet restore "BFF.Identidad.Infraestructura/BFF.Identidad.Infraestructura.csproj"

COPY ["BFF.Identidad.Aplicacion/BFF.Identidad.Aplicacion.csproj", "BFF.Identidad.Aplicacion/"]
#restore para las dependencias
RUN dotnet restore "BFF.Identidad.Aplicacion/BFF.Identidad.Aplicacion.csproj"

COPY ["BFF.Identidad.API/BFF.Identidad.API.csproj", "BFF.Identidad.API/"]
#restore para las dependencias
RUN dotnet restore "BFF.Identidad.API/BFF.Identidad.API.csproj"

#================================
#COPIAR FUENTES Y COMPILAR
#================================

COPY ./references /src/references/.

COPY ./BFF.Identidad.Dominio/ /src/BFF.Identidad.Dominio/.
WORKDIR "/src/BFF.Identidad.Dominio"
RUN dotnet build "./BFF.Identidad.Dominio.csproj" -c Release -o /app/build

WORKDIR "/src/BFF.Identidad.Infraestructura"
COPY ./BFF.Identidad.Infraestructura/ /src/BFF.Identidad.Infraestructura/.

WORKDIR "/src/BFF.Identidad.Infraestructura"
RUN dotnet build "./BFF.Identidad.Infraestructura.csproj" -c Release -o /app/build
#
WORKDIR "/src/BFF.Identidad.Aplicacion"
COPY ./BFF.Identidad.Aplicacion/ /src/BFF.Identidad.Aplicacion/.
WORKDIR "/src/BFF.Identidad.Aplicacion"
RUN dotnet build "./BFF.Identidad.Aplicacion.csproj" -c Release -o /app/build
#
WORKDIR "/src/BFF.Identidad.API"
COPY ./BFF.Identidad.API/ /src/BFF.Identidad.API
WORKDIR "/src/BFF.Identidad.API"
RUN dotnet build "./BFF.Identidad.API.csproj" -c Release -o /app/build


#
#
FROM build AS publish
#RUN dotnet publish "./BFF.Identidad.API.csproj"  -c Debug --self-contained --runtime linux-musl-x64 -o /app/publish 
RUN dotnet publish "./BFF.Identidad.API.csproj"  -c Release --self-contained -o /app/publish 
#
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

USER root
RUN cp /app/certificado-identidad/ca-chain.cert.pem /etc/ssl/certs/
RUN cp /app/certificado-identidad/iam.arquitectura.com.key.pem /etc/ssl/private/
RUN cp /app/certificado-identidad/iam.arquitectura.com.cert.pem /etc/ssl/certs/

RUN chown -R appuser /app/certificado-identidad
RUN update-ca-certificates
RUN apk update 
RUN apk upgrade --available

USER appuser

ENTRYPOINT [ "dotnet", "BFF.Identidad.API.dll" ]