#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BankingGWService/BankingGWService.csproj", "BankingGWService/"]
RUN dotnet restore "BankingGWService/BankingGWService.csproj"
COPY . .
WORKDIR "/src/BankingGWService"
RUN dotnet build "BankingGWService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankingGWService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankingGWService.dll"]