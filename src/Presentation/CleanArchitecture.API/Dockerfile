#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Presentation/CleanArchitecture.API/CleanArchitecture.API.csproj", "src/Presentation/CleanArchitecture.API/"]
COPY ["src/Core/CleanArchitecture.Application/CleanArchitecture.Application.csproj", "src/Core/CleanArchitecture.Application/"]
COPY ["src/Shared/CleanArchitecture.Shared/CleanArchitecture.Shared.csproj", "src/Shared/CleanArchitecture.Shared/"]
COPY ["src/Core/CleanArchitecture.Domain/CleanArchitecture.Domain.csproj", "src/Core/CleanArchitecture.Domain/"]
COPY ["src/Infrastructure/CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj", "src/Infrastructure/CleanArchitecture.Infrastructure/"]
COPY ["src/Infrastructure/CleanArchitecture.Persistence/CleanArchitecture.Persistence.csproj", "src/Infrastructure/CleanArchitecture.Persistence/"]
RUN dotnet restore "src/Presentation/CleanArchitecture.API/CleanArchitecture.API.csproj"
COPY . .
WORKDIR "/src/src/Presentation/CleanArchitecture.API"
RUN dotnet build "CleanArchitecture.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanArchitecture.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.API.dll"]