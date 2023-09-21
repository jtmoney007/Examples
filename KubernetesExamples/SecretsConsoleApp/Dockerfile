#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["KubernetesExamples/SecretsConsoleApp/SecretsConsoleApp.csproj", "KubernetesExamples/SecretsConsoleApp/"]
RUN dotnet restore "KubernetesExamples/SecretsConsoleApp/SecretsConsoleApp.csproj"
COPY . .
WORKDIR "/src/KubernetesExamples/SecretsConsoleApp"
RUN dotnet build "SecretsConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecretsConsoleApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecretsConsoleApp.dll"]