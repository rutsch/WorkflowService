FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as sdk
WORKDIR /src
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
COPY . .

# Required in current alpine for grpc compilation
RUN cp sgerrand.rsa.pub /etc/apk/keys/ && apk add --force-overwrite glibc-2.33-r0.apk
RUN apk fix --force-overwrite alpine-baselayout-data

#RUN wget -q -O /etc/apk/keys/sgerrand.rsa.pub https://alpine-pkgs.sgerrand.com/sgerrand.rsa.pub && wget https://github.com/sgerrand/alpine-pkg-glibc/releases/download/2.31-r0/glibc-2.31-r0.apk && apk add glibc-2.31-r0.apk

# Explicitly set target to linux-musl-x64 (alpine is musl-based) - even if dotnet --info shows this value
RUN dotnet publish -c Release -r linux-musl-x64 -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
COPY Taxxor_root.crt /usr/local/share/ca-certificates/
RUN addgroup -S -g 1000 aspnet && adduser -S -h /app -s /bin/sh -G aspnet -g "ASP.NET" -u 1000 aspnet && update-ca-certificates 2>/dev/null
WORKDIR /app
COPY --from=sdk /app .

RUN mkdir data logs && chown aspnet:aspnet data logs

USER aspnet:aspnet
EXPOSE 4831

ENV ASPNETCORE_URLS=
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1

ENTRYPOINT [ "dotnet", "WorkflowService.dll" ]

# Build from solution root with: docker build -t structureddatastore .
# Exposed port 4807
# Relevant paths:
#  /app/logs - logs
#  /app/data - database (sqlite)
