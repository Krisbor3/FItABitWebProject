#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FItABitWebProject/FItABitWebProject.csproj", "FItABitWebProject/"]
RUN dotnet restore "FItABitWebProject/FItABitWebProject.csproj"
COPY . .
WORKDIR "/src/FItABitWebProject"
RUN dotnet build "FItABitWebProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FItABitWebProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FItABitWebProject.dll"]