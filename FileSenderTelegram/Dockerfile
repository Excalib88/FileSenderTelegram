FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FileSenderTelegram/FileSenderTelegram.csproj", "FileSenderTelegram/"]
RUN dotnet restore "FileSenderTelegram/FileSenderTelegram.csproj"
COPY . .
WORKDIR "/src/FileSenderTelegram"
RUN dotnet build "FileSenderTelegram.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileSenderTelegram.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileSenderTelegram.dll"]
