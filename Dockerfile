# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "SOFT806.WebApp.dll"]