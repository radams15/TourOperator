FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -r linux-musl-x64 -o /out --self-contained true


FROM alpine:latest

ENV \
    ASPNETCORE_URLS=http://+:80 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

RUN apk add libstdc++ icu

WORKDIR /app

COPY --from=build /out .

EXPOSE 80

ENTRYPOINT [ "./TourOperator" ]
