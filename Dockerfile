FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR .

COPY ./*.sln ./
WORKDIR ./src
COPY ./src/**/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*} && mv $file ${file%.*}; done

WORKDIR ..
RUN dotnet restore
COPY . ./

ARG Configuration=Release
RUN dotnet build -c $Configuration -o /app

FROM build AS publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENV ASPNETCORE_URLS https://+:5001;http://+:5000
ENV ASPNETCORE_HTTPS_PORT=5001
ENV ASPNETCORE_ENVIRONMENT docker

ENTRYPOINT ["dotnet", "BankApplication.Api.dll"]
