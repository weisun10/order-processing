# order-processing
Order Processing System - C# Web API

## docker images

### build applications
```bash
cd ./src
docker build --no-cache -t webapis:latest -f Presentation/WebAPIs/Dockerfile .
docker build --no-cache -t ordereventservice:latest -f Presentation/OrderEventService/Dockerfile .
```

### docker compose
```bash
cd <solution root folder>
docker compose up -d
```

## applications
### webapi
C# Web API - [Swagger](http://127.0.0.1:5032/swagger/index.html)

Create order, write records into **Order** table, then publish **OrderCreatedEvent** event via RabbitMQ.

### ordereventservice
The service subscribes **OrderCreatedEvent** event and write records into **OrderEvent** table.
