FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AdvertBoard/Hosts/AdvertBoard.Hosts.Api/AdvertBoard.Hosts.Api.csproj", "src/AdvertBoard/Hosts/AdvertBoard.Hosts.Api/"]
COPY ["src/AdvertBoard/Infrastructure/AdvertBoard.Infrastructure.ComponentRegistrar/AdvertBoard.Infrastructure.ComponentRegistrar.csproj", "src/AdvertBoard/Infrastructure/AdvertBoard.Infrastructure.ComponentRegistrar/"]
COPY ["src/AdvertBoard/Infrastructure/AdvertBoard.Infrastructure.DataAccess/AdvertBoard.Infrastructure.DataAccess.csproj", "src/AdvertBoard/Infrastructure/AdvertBoard.Infrastructure.DataAccess/"]
COPY ["src/AdvertBoard/Domain/AdvertBoard.Domain/AdvertBoard.Domain.csproj", "src/AdvertBoard/Domain/AdvertBoard.Domain/"]
COPY ["src/AdvertBoard/Contracts/AdvertBoard.Contracts/AdvertBoard.Contracts.csproj", "src/AdvertBoard/Contracts/AdvertBoard.Contracts/"]
COPY ["src/AdvertBoard/Application/AdvertBoard.Application.AppServices/AdvertBoard.Application.AppServices.csproj", "src/AdvertBoard/Application/AdvertBoard.Application.AppServices/"]
COPY ["src/AdvertBoard/Infrastructure/AdvertBoard.Infrastructure/AdvertBoard.Infrastructure.csproj", "src/AdvertBoard/Infrastructure/AdvertBoard.Infrastructure/"]
RUN dotnet restore "src/AdvertBoard/Hosts/AdvertBoard.Hosts.Api/AdvertBoard.Hosts.Api.csproj"
COPY . .
WORKDIR "/src/src/AdvertBoard/Hosts/AdvertBoard.Hosts.Api"
RUN dotnet build "AdvertBoard.Hosts.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AdvertBoard.Hosts.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdvertBoard.Hosts.Api.dll"]
