FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
ARG SEEDING
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet tool install --global dotnet-ef --version 7.0.0
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "ef","database","update","--project","SOFT703A2.Infrastructure/SOFT703A2.Infrastructure.csproj","--startup-project","SOFT703A2.WebApp/SOFT703A2.WebApp.csproj"]
