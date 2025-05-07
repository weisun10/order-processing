# Order Processing System

This repository contains a C# Web API for an Order Processing System. The system includes two main applications: `WebAPIs` and `OrderEventService`.

## Docker Images

### Build Applications
To build the Docker images for the applications, run the following commands:

```bash
cd ./src
docker build --no-cache -t webapis:latest -f Presentation/WebAPIs/Dockerfile .
docker build --no-cache -t ordereventservice:latest -f Presentation/OrderEventService/Dockerfile .
```

### Start dependencies
To start infra dependencies (MS SQL Server and RabbitMQ), navigate to the solution root folder and run:

```bash
cd <solution root folder>
docker compose up -d
```

Listening ports:
- MS SQL Server - 1433
- RabbitMQ - 5672, 15672 (managment UI)

## Applications

### WebAPIs
The `WebAPIs` application is a C# Web API that allows you to create orders. It writes records into the **Order** table and publishes **OrderCreatedEvent** events via RabbitMQ.

- **Swagger UI**: [Swagger](http://127.0.0.1:5000/swagger/index.html)
- **Default Port**: 5000 (can be overridden via the `--url` command line parameter or the `ASPNETCORE_HTTP_PORTS` environment variable).

To run the application manually:

```bash
dotnet WebAPIs.dll
```

### OrderEventService
The `OrderEventService` application subscribes to **OrderCreatedEvent** events and writes records into the **OrderEvent** table.

To run the application manually:

```bash
dotnet OrderEventService.dll
```
