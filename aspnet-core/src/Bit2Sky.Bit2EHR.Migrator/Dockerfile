#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Bit2Sky.Bit2EHR.Migrator/Bit2Sky.Bit2EHR.Migrator.csproj", "src/Bit2Sky.Bit2EHR.Migrator/"]
COPY ["src/Bit2Sky.Bit2EHR.EntityFrameworkCore/Bit2Sky.Bit2EHR.EntityFrameworkCore.csproj", "src/Bit2Sky.Bit2EHR.EntityFrameworkCore/"]
COPY ["src/Bit2Sky.Bit2EHR.Core/Bit2Sky.Bit2EHR.Core.csproj", "src/Bit2Sky.Bit2EHR.Core/"]
COPY ["src/Bit2Sky.Bit2EHR.Core.Shared/Bit2Sky.Bit2EHR.Core.Shared.csproj", "src/Bit2Sky.Bit2EHR.Core.Shared/"]
RUN dotnet restore "src/Bit2Sky.Bit2EHR.Migrator/Bit2Sky.Bit2EHR.Migrator.csproj"
COPY . .
WORKDIR "/src/src/Bit2Sky.Bit2EHR.Migrator"
RUN dotnet build "Bit2Sky.Bit2EHR.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bit2Sky.Bit2EHR.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bit2Sky.Bit2EHR.Migrator.dll", "-s"]
