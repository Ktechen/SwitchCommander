FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SwitchCommander.Blazor/SwitchCommander.Blazor.csproj", "SwitchCommander.Blazor/"]
RUN dotnet restore "SwitchCommander.Blazor/SwitchCommander.Blazor.csproj"
COPY . .
WORKDIR "/src/SwitchCommander.Blazor"
RUN dotnet build "SwitchCommander.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SwitchCommander.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SwitchCommander.Blazor.dll"]
