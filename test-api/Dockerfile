FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["test-api/TestApi.csproj", "test-api/"]
RUN dotnet restore "test-api/TestApi.csproj"
COPY . .
WORKDIR "/src/test-api"
RUN dotnet build "TestApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestApi.dll"]
