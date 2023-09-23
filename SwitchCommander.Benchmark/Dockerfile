FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SwitchCommander.Benchmark/SwitchCommander.Benchmark.csproj", "SwitchCommander.Benchmark/"]
RUN dotnet restore "SwitchCommander.Benchmark/SwitchCommander.Benchmark.csproj"
COPY . .
WORKDIR "/src/SwitchCommander.Benchmark"
RUN dotnet build "SwitchCommander.Benchmark.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SwitchCommander.Benchmark.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SwitchCommander.Benchmark.dll"]
