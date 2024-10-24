#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ComelitApiGateway/ComelitApiGateway.csproj", "ComelitApiGateway/"]
COPY ["ComelitApiGateway.Commons/ComelitApiGateway.Commons.csproj", "ComelitApiGateway.Commons/"]
COPY ["ComelitApiGateway.Services/ComelitApiGateway.Services.csproj", "ComelitApiGateway.Services/"]
RUN dotnet restore "ComelitApiGateway/ComelitApiGateway.csproj"
COPY . .
WORKDIR "/src/ComelitApiGateway"
RUN dotnet build "ComelitApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ComelitApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ComelitApiGateway.dll"]