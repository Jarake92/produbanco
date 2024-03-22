FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.19  AS base

WORKDIR /app
EXPOSE 7200
EXPOSE 7201

ENV ASPNETCORE_URLS=https://+:7200;http://+:7201

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers

 
RUN apk add --no-cache nano 
RUN apk add --no-cache openssl
RUN apk add  --no-cache curl
RUN apk add busybox-extras
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.19 AS build


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
COPY ["src/cliente/api.cliente/api.cliente.csproj", "api.cliente/"]
# RUN dotnet restore "api.cliente/api.cliente.csproj"

COPY src/cliente/api.cliente/ api.cliente/.
WORKDIR "/src/services/api.cliente"
RUN dotnet build "api.cliente.csproj" -c Release -o /app/build


FROM build AS publish
#RUN dotnet publish "api.cliente.csproj"  -c Debug --self-contained --runtime linux-x64 -o /app/publish 
RUN dotnet publish "api.cliente.csproj"  -c Release --self-contained -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#================================
#CERTIFICADOS DE IDENTIDAD HTTPS
#================================
USER root
# CERTIFICADOS DEL SERVICIO 
RUN cp /app/certificado-identidad/ca-chain.cert.pem /etc/ssl/certs/
RUN cp /app/certificado-identidad/cliente.arquitectura.com.key.pem /etc/ssl/private/
RUN cp /app/certificado-identidad/cliente.arquitectura.com.cert.pem /etc/ssl/certs/

RUN chown -R appuser /app/certificado-identidad
RUN update-ca-certificates

RUN apk update 
RUN apk upgrade --available

USER appuser

ENTRYPOINT [ "dotnet", "api.cliente.dll" ] 
