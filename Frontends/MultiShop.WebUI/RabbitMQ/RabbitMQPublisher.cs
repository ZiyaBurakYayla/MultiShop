using RabbitMQ.Client;
using System.Text.Json;

namespace MultiShop.WebUI.RabbitMQ
{
    public class RabbitMQPublisher
    {
        public void Publish(OrderCreatedEvent orderEvent)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("OrderCreatedMailQueue", true, false, false, null);

            var body = JsonSerializer.SerializeToUtf8Bytes(orderEvent);
            channel.BasicPublish("", "OrderCreatedMailQueue", null, body);
        }
    }
}
