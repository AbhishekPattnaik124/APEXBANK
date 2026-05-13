# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy solution and project files
COPY *.sln ./
COPY src/ApexBank.Api/*.csproj ./src/ApexBank.Api/
COPY src/ApexBank.Application/*.csproj ./src/ApexBank.Application/
COPY src/ApexBank.Domain/*.csproj ./src/ApexBank.Domain/
COPY src/ApexBank.Infrastructure/*.csproj ./src/ApexBank.Infrastructure/

# Restore dependencies
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish src/ApexBank.Api/ApexBank.Api.csproj -c Release -o out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose port 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ApexBank.Api.dll"]
