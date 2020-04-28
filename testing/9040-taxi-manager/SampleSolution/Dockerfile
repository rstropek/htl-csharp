# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# Copy csproj and restore as distinct layers.
# Note that UI is ignored because it is not built in Docker.
COPY *.sln .
COPY TaxiManager.Shared/*.csproj ./TaxiManager.Shared/
COPY TaxiManager.Tests/*.csproj ./TaxiManager.Tests/
COPY TaxiManager.WebApi/*.csproj ./TaxiManager.WebApi/
RUN dotnet sln TaxiManager.sln remove ./TaxiManager.UI/TaxiManager.UI.csproj && dotnet restore

# Copy everything else and test app
COPY . .
ENV ConnectionStrings__DefaultConnection="Data Source=db;User=sa;Password=yourStrongP@ssw0rd"
WORKDIR /source/TaxiManager.Tests
RUN dotnet test --no-restore

# Publish Web API
WORKDIR /source/TaxiManager.WebApi
RUN dotnet publish -c release -o /app --no-restore

# Final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "TaxiManager.WebApi.dll"]
