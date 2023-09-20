#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["KubernetesExamples/SimpleConsoleApp/SimpleConsoleApp.csproj", "KubernetesExamples/SimpleConsoleApp/"]
RUN dotnet restore "KubernetesExamples/SimpleConsoleApp/SimpleConsoleApp.csproj"
COPY . .
WORKDIR "/src/KubernetesExamples/SimpleConsoleApp"
RUN dotnet build "SimpleConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleConsoleApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleConsoleApp.dll"]
