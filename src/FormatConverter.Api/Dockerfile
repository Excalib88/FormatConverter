FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
ADD FormatConverter.db /app
COPY /bin/Debug/netcoreapp3.1/publish /app
ENTRYPOINT ["dotnet", "FormatConverter.Api.dll"]