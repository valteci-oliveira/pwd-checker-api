# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files
COPY ["src/pwd-checker-api/pwd-checker-api.csproj", "src/pwd-checker-api/"]
COPY ["src/pwd-checker-api-test/pwd-checker-api-test.csproj", "src/pwd-checker-api-test/"]

# Restore dependencies
RUN dotnet restore "src/pwd-checker-api/pwd-checker-api.csproj"
RUN dotnet restore "src/pwd-checker-api-test/pwd-checker-api-test.csproj"

# Copy solution and source code
COPY . .

# Build application
WORKDIR "/src/src/pwd-checker-api"
RUN dotnet build "pwd-checker-api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "pwd-checker-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copy published application
COPY --from=publish /app/publish .

# Environment variables
ENV ASPNETCORE_URLS=http://+:5238
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 5238

ENTRYPOINT ["dotnet", "pwd-checker-api.dll"]
