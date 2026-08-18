using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MultiShop.RabbitMQMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateMessage()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("Kuyruk1",true,false,false,arguments: null);

            var messageContent = "Merhaba";
            var byteMessageContent = Encoding.UTF8.GetBytes(messageContent);

            channel.BasicPublish(exchange: "", routingKey: "Kuyruk1",basicProperties: null,body:byteMessageContent);
                
            return Ok("Mesaj kuyruğa alındı");
        }

        private static string message;
        [HttpGet]
        public IActionResult ReadMessage()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, body) =>
            {
                var byteMessage = body.Body.ToArray();
                message = Encoding.UTF8.GetString(byteMessage);
            };

            channel.BasicConsume(queue :"Kuyruk1",autoAck: false,consumer:consumer);

            if(string.IsNullOrEmpty(message))
            {
                return NoContent();
            }
            else
            {
                return Ok(message);
            }
        }

    }
}
