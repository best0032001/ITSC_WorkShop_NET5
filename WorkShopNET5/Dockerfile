FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY [".", "."]
RUN dotnet restore "WorkShopNET5.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "WorkShopNET5.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WorkShopNET5.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WorkShopNET5.dll"]
