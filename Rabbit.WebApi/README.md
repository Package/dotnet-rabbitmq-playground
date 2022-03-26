# RabbitMQ Playground in .NET Core

Sample application which uses RabbitMQ as a message broker between two services in .NET Core 6.

`Rabbit.WebApi` - a .NET Core Web API which responds to `POST` requests to the following endpoints:

* `api/v1/customers`


_Sample Request Body:_

```json
{
  "FirstName": "John",
  "LastName": "Smith",
  "EmailAddress": "john@smith.com"
}
```

* `api/v1/orders`

_Sample Request Body:_

```json
{
  "ProductName": "Salad Box",
  "ProductPrice": 4.25
}
```

Successful `POST` requests to these endpoints will return the created domain entity (`Customer` or `Order`) and serialize the object into RabbitMQ Queue.

`Rabbit.Consumer` - a .NET Core Worker Service (Background) which subscribes to incoming messages for both the `Customers` and `Orders` queues and
logs information to the console.

`Rabbit.Domain` - a .NET Core shared class library for common domain objects shared between the services.