# ==========================
# Stage 1 - Build
# ==========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy project file
COPY *.csproj ./

# Restore dependencies
RUN dotnet restore

# Copy remaining source code
COPY . .

# Publish application
RUN dotnet publish -c Release -o /app/publish --no-restore

# ==========================
# Stage 2 - Runtime
# ==========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet","_NET-Practice-1.dll"]