using System.Text;
using System.Text.Json;
using MultiShop.Catalog.Services.ProductServices;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MultiShop.Catalog.RabbitMQ
{
    public class ProductStockReduceConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;

        public ProductStockReduceConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("ProductStockReduceQueue", true, false, false, null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, args) =>
            {
                var body = args.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var stockEvent = JsonSerializer.Deserialize<ProductStockReduceEvent>(json);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                    await productService.ReduceStockAsync(stockEvent.Sku, stockEvent.Quantity);
                }

                _channel.BasicAck(args.DeliveryTag, false);
            };

            _channel.BasicConsume("ProductStockReduceQueue", false, consumer);
            return Task.CompletedTask;
        }
    }
}
