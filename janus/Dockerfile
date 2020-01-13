FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS runtime
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["*.csproj", "janus/"]
RUN dotnet restore "janus/janus.csproj"
COPY . "janus"
WORKDIR "/src/janus"
RUN dotnet build "janus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "janus.csproj" -c Release -o /app/publish

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "janus.dll"]