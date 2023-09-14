FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SwitchPrototype.Api/SwitchPrototype.Api.csproj", "SwitchPrototype.Api/"]
RUN dotnet restore "SwitchPrototype.Api/SwitchPrototype.Api.csproj"
COPY . .
WORKDIR "/src/SwitchPrototype.Api"
RUN dotnet build "SwitchPrototype.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SwitchPrototype.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SwitchPrototype.Api.dll"]
