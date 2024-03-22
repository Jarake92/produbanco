FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

WORKDIR /app
EXPOSE 7400
EXPOSE 7401

ENV ASPNETCORE_URLS=http://+:7401;https://+:7400

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
RUN dotnet restore "comun/shared.comun.csproj"

COPY src/comun/ comun/.
WORKDIR "/src/comun"
RUN dotnet build "shared.comun.csproj" -c Release -o /app/build

#================================
#INFORMACION DE SERVICE
#================================

WORKDIR /src/services 
COPY ["src/telefono/api.telefono/api.telefono.csproj", "api.telefono/"]
RUN dotnet restore "api.telefono/api.telefono.csproj"

COPY src/telefono/api.telefono/ api.telefono/.
WORKDIR "/src/services/api.telefono"
RUN dotnet build "api.telefono.csproj" -c Release -o /app/build

FROM build AS publish
#RUN dotnet publish "api.telefono.csproj"  -c Release --self-contained --runtime linux-musl-x64 -o /app/publish 
RUN dotnet publish "api.telefono.csproj"  -c Release --self-contained -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#================================
#CERTIFICADOS DE IDENTIDAD HTTPS
#================================
USER root
# CERTIFICADOS DEL SERVICIO 
RUN cp /app/certificado-identidad/ca-chain.cert.pem /etc/ssl/certs/
RUN cp /app/certificado-identidad/telefono.arquitectura.com.key.pem /etc/ssl/private/
RUN cp /app/certificado-identidad/telefono.arquitectura.com.cert.pem /etc/ssl/certs/

RUN chown -R appuser /app/certificado-identidad
RUN update-ca-certificates
USER appuser


ENTRYPOINT [ "dotnet", "api.telefono.dll" ]