FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

WORKDIR /app
EXPOSE 7100
EXPOSE 7101

ENV ASPNETCORE_URLS=https://+:7100;http://+:7101

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN apk add --no-cache nano 
RUN apk add --no-cache openssl
RUN apk add  --no-cache curl
RUN apk add busybox-extras

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build


#================================
#INFORMACION DE COMUN
#================================
WORKDIR /src
COPY ["src/comun/shared.comun.csproj", "comun/"]
#RUN dotnet restore "comun/shared.comun.csproj"

COPY src/comun/ comun/.
WORKDIR "/src/comun"
RUN dotnet build "shared.comun.csproj" -c Release -o /app/build

#================================
#INFORMACION DE SERVICE
#================================

WORKDIR /src/services 
COPY ["src/orquestador/api.orquestador/api.orquestador.csproj", "api.orquestador/"]
#RUN dotnet restore "api.orquestador/api.orquestador.csproj"

COPY src/orquestador/api.orquestador/ api.orquestador/.
WORKDIR "/src/services/api.orquestador"
RUN dotnet build "api.orquestador.csproj" -c Release -o /app/build

FROM build AS publish
#RUN dotnet publish "api.orquestador.csproj"  -c Release --self-contained --runtime linux-musl-x64 -o /app/publish 
RUN dotnet publish "api.orquestador.csproj"  -c Release --self-contained -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#================================
#CERTIFICADOS DE IDENTIDAD HTTPS
#================================
USER root
# CERTIFICADOS DEL SERVICIO 
RUN cp /app/certificado-identidad/ca-chain.cert.pem /etc/ssl/certs/
RUN cp /app/certificado-identidad/orquestador.arquitectura.com.key.pem /etc/ssl/private/
RUN cp /app/certificado-identidad/orquestador.arquitectura.com.cert.pem /etc/ssl/certs/

RUN chown -R appuser /app/certificado-identidad
RUN update-ca-certificates
USER appuser

ENTRYPOINT [ "dotnet", "api.orquestador.dll" ]