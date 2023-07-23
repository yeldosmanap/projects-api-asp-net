FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProductsAPI.Api/ProductsAPI.Api.csproj", "ProductsAPI.Api/"]
RUN dotnet restore "ProductsAPI.Api/ProductsAPI.Api.csproj"
COPY . .
WORKDIR "/src/ProductsAPI.Api"
RUN dotnet build "ProductsAPI.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductsAPI.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductsAPI.Api.dll"]
