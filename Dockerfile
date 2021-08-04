#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MarketView/MarketView.Services.csproj", "MarketView/"]
COPY ["MarketView.Models/MarketView.Models.csproj", "MarketView.Models/"]
COPY ["MarketView.Engine/MarketView.Engine.csproj", "MarketView.Engine/"]
COPY ["MarketView.Data/MarketView.Data.csproj", "MarketView.Data/"]
COPY ["MarketView.Commons/MarketView.Commons.csproj", "MarketView.Commons/"]
RUN dotnet restore "MarketView/MarketView.Services.csproj"
COPY . .
WORKDIR "/src/MarketView"
RUN dotnet build "MarketView.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarketView.Services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarketView.Services.dll"]