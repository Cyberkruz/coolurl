FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Metamask/*.csproj ./Metamask/
COPY Metamask.Web/*.csproj ./Metamask.Web/
RUN dotnet restore

# copy everything else and build app
COPY Metamask/. ./Metamask/
COPY Metamask.Web/. ./Metamask.Web/
WORKDIR /app/Metamask.Web
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /app/Metamask.Web/out ./
ENTRYPOINT ["dotnet", "Metamask.Web.dll"]