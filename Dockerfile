#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DocTemplate.csproj", "."]
RUN dotnet restore "./DocTemplate.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DocTemplate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocTemplate.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN apt-get update; apt-get install -y libicu-dev libharfbuzz0b libfontconfig1 fontconfig
RUN apt-get install -y apt-utils libgdiplus libc6-dev
COPY ./fonts /usr/share/fonts/truetype/ms
RUN fc-cache -f -v

WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "DocTemplate.dll"]