#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["S3-Service/S3-Service.csproj", "S3-Service/"]
RUN dotnet restore "S3-Service/S3-Service.csproj"
COPY . .
WORKDIR "/src/S3-Service"
RUN dotnet build "S3-Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "S3-Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "S3-Service.dll"]