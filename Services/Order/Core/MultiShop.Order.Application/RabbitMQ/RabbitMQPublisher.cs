using RabbitMQ.Client;
using System.Text.Json;

namespace MultiShop.Order.Application.RabbitMQ
{
    public class RabbitMQPublisher
    {
        public void Publish(ProductStockReduceEvent stockEvent)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("ProductStockReduceQueue", true, false, false, null);

            var body = JsonSerializer.SerializeToUtf8Bytes(stockEvent);
            channel.BasicPublish("", "ProductStockReduceQueue", null, body);
        }
    }
}
