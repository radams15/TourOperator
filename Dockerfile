FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -r linux-musl-x64 -o /out --self-contained true


# FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS release 
FROM alpine:latest

ENV \
    ASPNETCORE_URLS=http://+:80 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

RUN apk add libstdc++

WORKDIR /app

COPY --from=build /out .

EXPOSE 80

ENTRYPOINT [ "./TourOperator" ]
