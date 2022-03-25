FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Rabbit.Consumer/Rabbit.Consumer.csproj", "Rabbit.Consumer/"]
RUN dotnet restore "Rabbit.Consumer/Rabbit.Consumer.csproj"
COPY . .
WORKDIR "/src/Rabbit.Consumer"
RUN dotnet build "Rabbit.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rabbit.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rabbit.Consumer.dll"]
