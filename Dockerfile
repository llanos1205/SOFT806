# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
ARG SEEDING
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the .NET SDK for building our application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG SEEDING
WORKDIR /src
COPY . .
RUN dotnet restore

WORKDIR "/src/SOFT703A2.WebApp"
RUN dotnet build "SOFT806.WebApp.csproj" -c Release -o /app/build --no-restore

# Publish the application
FROM build AS publish
RUN dotnet publish "SOFT806.WebApp.csproj" -c Release -o /app/publish

# Final stage / image
FROM base AS final
ARG SEEDING
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOFT806.WebApp.dll"]