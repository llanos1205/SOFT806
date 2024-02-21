FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SOFT806.WebApp/SOFT806.WebApp.csproj", "SOFT806.WebApp/"]
RUN dotnet restore "SOFT806.WebApp/SOFT806.WebApp.csproj"
COPY . .
WORKDIR "/src/SOFT806.WebApp"
RUN dotnet build "SOFT806.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SOFT806.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOFT806.WebApp.dll"]
