# Order Processing System

This repository contains a **mock** Order Processing System implemented in C# for **learning purposes**.

The system consists of three applications: `WebAPIs`, `OrderEventService`, and `OrderPages`.

## WebAPIs
The `WebAPIs` project is a C# Web API that exposes a **POST** endpoint (`/api/orders`) to create orders. It stores orders in the **Orders** table and publishes **OrderCreatedEvent** messages via RabbitMQ.

## OrderEventService
The `OrderEventService` is a console application (with a hosted service) that listens for **OrderCreatedEvent** messages and writes records into the **OrderEvents** table.

## OrderPages
The `OrderPages` is a Blazor Server application that displays the latest data from the `Orders` and `OrderEvents` tables.

## Dependencies
The system depends on **RabbitMQ** and **SQL Server**.

It is recommended to run these services as **Docker containers**. Refer to the `"mssql"` and `"rabbitmq"` services defined in **compose.yaml**.

## Database Schema
Run the following commands to create the database schema:
```bash
dotnet ef database update --context OrderDbContext -p src/Infrastructure -s src/Presentation/WebAPIs
dotnet ef database update --context OrderEventDbContext -p src/Infrastructure -s src/Presentation/OrderEventService
```

## Configuration
Refer to **`src/Presentation/appsettings.json`** for details on logging, credentials, and connection strings.

For containerized applications, use **`src/Presentation/appsettings-container.json`**, which differs only in **hostname resolution** (`127.0.0.1` vs container name).

### Ports
- **WebAPIs** - `5032`
- **OrderPages** - `5033`
- **MS SQL Server** - `1433`
- **RabbitMQ** - `5672`, `15672` (Management UI)

## Running the System

### Start Applications (and Dependencies) with Docker Compose
To start the applications along with their dependencies (**SQL Server** and **RabbitMQ**), navigate to the solution root folder and run:

```sh
docker compose build
```
Once the build completes successfully, start the system with:
```sh
docker compose up -d
```

### Start Applications Manually
```sh
dotnet WebAPIs.dll
dotnet OrderEventService.dll
dotnet OrderPages.dll
```

### Interacting with the System
#### WebAPIs
[Swagger Documentation](http://127.0.0.1:5032/swagger/index.html)

Sample payload:
```json
{
  "customerId": "123456",
  "items": [
    {
      "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantity": 3
    },
    {
      "productId": "4fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantity": 4
    }
  ]
}
```

#### OrderPages
[OrderPages UI](http://127.0.0.1:5033/orders)

### Notes

1. The `ordereventservice` service has two replicas.
2. To view OrderEvent data, you can:
    - Check the output logs from OrderEventService containers.
    - Connect to the Orders database using your favorite database client.
    - Open the OrderPages UI. It automatically displays the latest data (leveraging the "InteractiveServer" render mode).
3. Docker compose commands can be used to start/stop specific services. For example: `docker compose down orderpages`.
