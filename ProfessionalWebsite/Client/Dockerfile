FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ProfessionalWebsite.Client.csproj .
RUN dotnet restore ProfessionalWebsite.Client.csproj
COPY . .
RUN dotnet build ProfessionalWebsite.Client.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish ProfessionalWebsite.Client.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .