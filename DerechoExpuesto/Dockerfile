# ============================================
# ETAPA 1 - COMPILACIÓN
# ============================================

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src


# Copiamos primero el proyecto para aprovechar
# la cache de restauración de paquetes NuGet.

COPY DerechoExpuesto.csproj ./


# Restaurar dependencias

RUN dotnet restore "DerechoExpuesto.csproj"


# Copiar el resto del proyecto

COPY . .


# Publicar aplicación en modo Release

RUN dotnet publish "DerechoExpuesto.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore


# ============================================
# ETAPA 2 - PRODUCCIÓN
# ============================================

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app


# Copiar solamente los archivos publicados

COPY --from=build /app/publish .


# Puerto interno utilizado por ASP.NET Core.
# Railway podrá enrutar tráfico hacia este puerto.

EXPOSE 8080


# ============================================
# EJECUTAR APLICACIÓN
# ============================================

ENTRYPOINT ["dotnet", "DerechoExpuesto.dll"]