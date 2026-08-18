using System.Text;
using System.Text.Json;
using MultiShop.Mail.Dtos;
using MultiShop.Mail.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MultiShop.Mail.RabbitMQ
{
    public class OrderCreatedMailConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;

        public OrderCreatedMailConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("OrderCreatedMailQueue", true, false, false, null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(json);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var mailService = scope.ServiceProvider.GetRequiredService<IMailService>();
                    mailService.SendMail(new SendMailDto
                    {
                        To = orderEvent.CustomerEmail,
                        Subject = "Siparişiniz Alındı - MultiShop",
                        Body = BuildBody(orderEvent)
                    });
                }

                _channel.BasicAck(args.DeliveryTag, false);
            };

            _channel.BasicConsume("OrderCreatedMailQueue", false, consumer);
            return Task.CompletedTask;
        }

        private string BuildBody(OrderCreatedEvent orderEvent)
        {
            string rows = "";
            foreach (var item in orderEvent.Items)
            {
                rows += "<tr>" +
                        "<td style='padding:8px;border:1px solid #eee;'>" + item.ProductName + "</td>" +
                        "<td style='padding:8px;border:1px solid #eee;text-align:center;'>" + item.Quantity + "</td>" +
                        "<td style='padding:8px;border:1px solid #eee;text-align:right;'>" + (item.Price * item.Quantity) + " ₺</td>" +
                        "</tr>";
            }

            return "<div style='font-family:Arial,sans-serif;color:#333;max-width:600px;margin:auto;border:1px solid #eee;'>" +
                   "<div style='background:#d19c97;color:#fff;padding:20px;text-align:center;'><h2 style='margin:0;'>MultiShop</h2></div>" +
                   "<div style='padding:20px;'>" +
                   "<h3>Siparişiniz Alındı</h3>" +
                   "<p>Merhaba " + orderEvent.CustomerName + ", siparişiniz başarıyla oluşturuldu.</p>" +
                   "<table style='border-collapse:collapse;width:100%;margin-top:10px;'>" +
                   "<tr style='background:#f5f5f5;'>" +
                   "<th style='padding:8px;border:1px solid #eee;text-align:left;'>Ürün</th>" +
                   "<th style='padding:8px;border:1px solid #eee;'>Adet</th>" +
                   "<th style='padding:8px;border:1px solid #eee;text-align:right;'>Tutar</th></tr>" +
                   rows +
                   "</table>" +
                   "<p style='margin-top:15px;font-size:16px;'><strong>Toplam Tutar: " + orderEvent.TotalPrice + " ₺</strong></p>" +
                   "<p style='color:#777;'>İşlem Numarası: " + orderEvent.TransactionId + "<br>Tarih: " + orderEvent.OrderDate.ToString("dd.MM.yyyy HH:mm") + "</p>" +
                   "<p>Bizi tercih ettiğiniz için teşekkür ederiz.</p></div>" +
                   "<div style='background:#f5f5f5;padding:12px;text-align:center;color:#999;font-size:12px;'>MultiShop &copy; " + orderEvent.OrderDate.Year + "</div>" +
                   "</div>";
        }
    }
}
