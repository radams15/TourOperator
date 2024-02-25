FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o /out


FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS release

WORKDIR /app

COPY --from=build /out .

EXPOSE 80

ENTRYPOINT [ "./TourOperator" ]
