FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Rabbit/Rabbit.csproj", "Rabbit/"]
RUN dotnet restore "Rabbit/Rabbit.csproj"
COPY . .
WORKDIR "/src/Rabbit"
RUN dotnet build "Rabbit.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rabbit.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rabbit.dll"]
