FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
ARG SEEDING
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet tool install --global dotnet-ef --version 7.0.0
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "ef","database","update","--project","SOFT806.Infrastructure/SOFT806.Infrastructure.csproj","--startup-project","SOFT806.WebApp/SOFT806.WebApp.csproj"]
