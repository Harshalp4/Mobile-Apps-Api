#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Bit2Sky.Bit2EHR.Web.Public/Bit2Sky.Bit2EHR.Web.Public.csproj", "src/Bit2Sky.Bit2EHR.Web.Public/"]
COPY ["src/Bit2Sky.Bit2EHR.Web.Core/Bit2Sky.Bit2EHR.Web.Core.csproj", "src/Bit2Sky.Bit2EHR.Web.Core/"]
COPY ["src/Bit2Sky.Bit2EHR.Application/Bit2Sky.Bit2EHR.Application.csproj", "src/Bit2Sky.Bit2EHR.Application/"]
COPY ["src/Bit2Sky.Bit2EHR.Application.Shared/Bit2Sky.Bit2EHR.Application.Shared.csproj", "src/Bit2Sky.Bit2EHR.Application.Shared/"]
COPY ["src/Bit2Sky.Bit2EHR.Core.Shared/Bit2Sky.Bit2EHR.Core.Shared.csproj", "src/Bit2Sky.Bit2EHR.Core.Shared/"]
COPY ["src/Bit2Sky.Bit2EHR.Core/Bit2Sky.Bit2EHR.Core.csproj", "src/Bit2Sky.Bit2EHR.Core/"]
COPY ["src/Bit2Sky.Bit2EHR.EntityFrameworkCore/Bit2Sky.Bit2EHR.EntityFrameworkCore.csproj", "src/Bit2Sky.Bit2EHR.EntityFrameworkCore/"]
COPY ["src/Bit2Sky.Bit2EHR.GraphQL/Bit2Sky.Bit2EHR.GraphQL.csproj", "src/Bit2Sky.Bit2EHR.GraphQL/"]
RUN dotnet restore "src/Bit2Sky.Bit2EHR.Web.Public/Bit2Sky.Bit2EHR.Web.Public.csproj"
COPY . .
WORKDIR "/src/src/Bit2Sky.Bit2EHR.Web.Public"
RUN dotnet build "Bit2Sky.Bit2EHR.Web.Public.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bit2Sky.Bit2EHR.Web.Public.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bit2Sky.Bit2EHR.Web.Public.dll"]
