FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Zar.sln ./

COPY Application/*.csproj ./Application/
COPY Shared/*.csproj ./Shared/
COPY Core/*.csproj ./Core/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY Web/*.csproj ./Web/

RUN dotnet restore
COPY . .
WORKDIR /src/Application
RUN dotnet build -c Release -o /app
WORKDIR /src/Shared
RUN dotnet build -c Release -o /app
WORKDIR /src/Core
RUN dotnet build -c Release -o /app
WORKDIR /src/Infrastructure
RUN dotnet build -c Release -o /app

WORKDIR /src/Web
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Web.dll"]