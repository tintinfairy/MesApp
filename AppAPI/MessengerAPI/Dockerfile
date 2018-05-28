FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY MessengerAPI/MessengerAPI.csproj MessengerAPI/
RUN dotnet restore MessengerAPI/MessengerAPI.csproj
COPY . .
WORKDIR /src/MessengerAPI
RUN dotnet build MessengerAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MessengerAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MessengerAPI.dll"]
