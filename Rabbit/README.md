# RabbitMQ Playground in .NET Core

Sample application which demonstrates using RabbitMQ for as a message bus between two services in .NET Core.

`Rabbit` - a Web API project which responds to `POST` requests for the following:

* `api/v1/customers`

```json
{
  "FirstName": "John",
  "LastName": "Smith",
  "EmailAddress": "john@smith.com"
}
```

* `api/v1/orders`

```json
{
  "ProductName": "Salad Box",
  "ProductPrice": 4.25
}
```

Successful `POST` requests to these endpoints will write a message into RabbitMQ.

`Rabbit.Consumer` - a .NET Core Background Service project which listens for incoming messages in the queue and
logs to the console.