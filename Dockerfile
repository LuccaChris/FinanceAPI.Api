# ===============================
# Build
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os csproj para cache de restore
COPY FinanceAPI/FinanceAPI.csproj FinanceAPI/
COPY FinanceAPI.Application/FinanceAPI.Application.csproj FinanceAPI.Application/
COPY FinanceAPI.Domain/FinanceAPI.Domain.csproj FinanceAPI.Domain/
COPY FinanceAPI.Infrastructure/FinanceAPI.Infrastructure.csproj FinanceAPI.Infrastructure/

# Restore
RUN dotnet restore FinanceAPI/FinanceAPI.csproj

# Copia todo o resto
COPY . .

# Publica
RUN dotnet publish FinanceAPI/FinanceAPI.csproj -c Release -o /app/publish

# ===============================
# Runtime
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Render injeta PORT automaticamente
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FinanceAPI.dll"]