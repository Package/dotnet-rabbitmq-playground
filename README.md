# RabbitMQ Playground in .NET Core

Sample application which uses RabbitMQ as a message broker between two services in .NET Core.

`Rabbit` - a .NET Core Web API which responds to `POST` requests to the following endpoints:

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

Successful `POST` requests to these endpoints will write a message into RabbitMQ Queue.

`Rabbit.Consumer` - a .NET Core Worker Service (Background) which subscribes to incoming messages in the queues and
logs information.

`Rabbit.Domain` - a .NET Core shared class library DLL for common domain objects referenced by the other projects.
