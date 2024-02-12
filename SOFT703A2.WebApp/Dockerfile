FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SOFT703A2.WebApp/SOFT703A2.WebApp.csproj", "SOFT703A2.WebApp/"]
RUN dotnet restore "SOFT703A2.WebApp/SOFT703A2.WebApp.csproj"
COPY . .
WORKDIR "/src/SOFT703A2.WebApp"
RUN dotnet build "SOFT703A2.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SOFT703A2.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOFT703A2.WebApp.dll"]
