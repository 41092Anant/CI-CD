# ---- Build Stage ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["CICD/CICD.csproj", "CICD/"]
RUN dotnet restore "CICD/CICD.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/CICD"
RUN dotnet publish "CICD.csproj" -c Release -o /app/publish

# ---- Runtime Stage ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CICD.dll"]