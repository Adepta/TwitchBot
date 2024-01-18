FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mitsyii_Chatbot/Mitsyii_Chatbot.csproj", "Mitsyii_Chatbot/"]
COPY ["Mitsyii_Services/Mitsyii_Services.csproj", "Mitsyii_Services/"]
RUN dotnet restore "Mitsyii_Chatbot/Mitsyii_Chatbot.csproj"
COPY . .
WORKDIR "/src/Mitsyii_Chatbot"
RUN dotnet build "Mitsyii_Chatbot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mitsyii_Chatbot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mitsyii_Chatbot.dll"]
