#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MovieService/MovieService.csproj", "MovieService/"]
COPY ["MovieService.Business/MovieService.Business.csproj", "MovieService.Business/"]
COPY ["MovieService.Domain/MovieService.Domain.csproj", "MovieService.Domain/"]
COPY ["MovieService.Infrastructure/MovieService.Infrastructure.csproj", "MovieService.Infrastructure/"]
RUN dotnet restore "MovieService/MovieService.csproj"
COPY . .
WORKDIR "/src/MovieService"
RUN dotnet build "MovieService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieService.dll"]