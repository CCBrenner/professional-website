#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Build from the ProfessionalWebsite folder so the referenced Client project is in the context:
#   docker build -f ProfessionalWebsite/Server/Dockerfile ProfessionalWebsite

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Server/ProfessionalWebsite.Server.csproj", "Server/"]
COPY ["Client/ProfessionalWebsite.Client.csproj", "Client/"]
RUN dotnet restore "Server/ProfessionalWebsite.Server.csproj"
COPY Server/ Server/
COPY Client/ Client/
RUN dotnet build "Server/ProfessionalWebsite.Server.csproj" -c Release -o /app/build /p:UseAppHost=false

FROM build AS publish
RUN dotnet publish "Server/ProfessionalWebsite.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "ProfessionalWebsite.Server.dll"]
