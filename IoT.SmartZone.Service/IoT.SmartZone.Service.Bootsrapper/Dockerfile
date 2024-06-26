#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IoT.SmartZone.Service.Bootsrapper/IoT.SmartZone.Service.Bootsrapper.csproj", "IoT.SmartZone.Service.Bootsrapper/"]
COPY ["IoT.SmartZone.Service.Modules.Users.Api/IoT.SmartZone.Service.Modules.Users.Api.csproj", "IoT.SmartZone.Service.Modules.Users.Api/"]
COPY ["IoT.SmartZone.Service.Modules.Users.Core/IoT.SmartZone.Service.Modules.Users.Core.csproj", "IoT.SmartZone.Service.Modules.Users.Core/"]
COPY ["IoT.SmartZone.Service.Modules.Users.Infrastructure/IoT.SmartZone.Service.Modules.Users.Infrastructure.csproj", "IoT.SmartZone.Service.Modules.Users.Infrastructure/"]
COPY ["IoT.SmartZone.Service.Shared.Abstractions/IoT.SmartZone.Service.Shared.Abstractions.csproj", "IoT.SmartZone.Service.Shared.Abstractions/"]
COPY ["IoT.SmartZone.Service.Shared.Infrastucture/IoT.SmartZone.Service.Shared.Infrastucture.csproj", "IoT.SmartZone.Service.Shared.Infrastucture/"]
RUN dotnet restore "./IoT.SmartZone.Service.Bootsrapper/IoT.SmartZone.Service.Bootsrapper.csproj"
COPY . .
WORKDIR "/src/IoT.SmartZone.Service.Bootsrapper"
RUN dotnet build "./IoT.SmartZone.Service.Bootsrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IoT.SmartZone.Service.Bootsrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IoT.SmartZone.Service.Bootsrapper.dll"]