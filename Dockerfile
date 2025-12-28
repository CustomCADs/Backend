FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /source

# Internal/Versioned source code
COPY CustomCADs.slnx .
COPY Directory.Packages.props .
COPY Directory.Build.props .
COPY src/ src/
COPY tests/ tests/

# External/Dynamic source code
RUN dotnet run --project src/Tools/CodeGen -- codegen write
RUN dotnet restore

# Compile all source code
RUN dotnet build -c $BUILD_CONFIGURATION

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]