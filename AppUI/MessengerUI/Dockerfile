FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 51717
EXPOSE 44344

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY MessengerUI/MessengerUI.csproj MessengerUI/
RUN dotnet restore MessengerUI/MessengerUI.csproj
COPY . .
WORKDIR /src/MessengerUI
RUN dotnet build MessengerUI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MessengerUI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MessengerUI.dll"]
