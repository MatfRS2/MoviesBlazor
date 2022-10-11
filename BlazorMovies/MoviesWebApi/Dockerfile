#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MoviesWebApi/MoviesWebApi.csproj", "MoviesWebApi/"]
RUN dotnet restore "MoviesWebApi/MoviesWebApi.csproj"
COPY . .
WORKDIR "/src/MoviesWebApi"
RUN dotnet build "MoviesWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MoviesWebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoviesWebApi.dll"]