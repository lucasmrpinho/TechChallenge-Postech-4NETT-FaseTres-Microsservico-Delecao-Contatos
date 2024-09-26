FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5010

ENV ASPNETCORE_URLS=http://+:5010
ENV ASPNETCORE_HTTP_PORTS=5010

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Postech.GroupEight.TechChallenge.ContactDelete.Api/Postech.GroupEight.TechChallenge.ContactDelete.Api.csproj", "src/Postech.GroupEight.TechChallenge.ContactDelete.Api/"]
RUN dotnet restore "src/Postech.GroupEight.TechChallenge.ContactDelete.Api/Postech.GroupEight.TechChallenge.ContactDelete.Api.csproj"
COPY . .
WORKDIR "/src/src/Postech.GroupEight.TechChallenge.ContactDelete.Api"
RUN dotnet build "Postech.GroupEight.TechChallenge.ContactDelete.Api.csproj" -c "$configuration" -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Postech.GroupEight.TechChallenge.ContactDelete.Api.csproj" \
    -c "$configuration" \
    -o /app/publish \
    /p:UseAppHost=false

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Postech.GroupEight.TechChallenge.ContactDelete.Api.dll"]