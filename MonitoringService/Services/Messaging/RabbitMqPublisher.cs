using RabbitMQ.Client;
using System.Text;

namespace MonitoringService.Services.Messaging
{
    public class RabbitMqPublisher : IMessagePublisher
    {
        private readonly IConfiguration _configuration;

        public RabbitMqPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void PublishAlert(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMq:Host"],
                Port = int.Parse(_configuration["RabbitMq:Port"] ?? "5672"),
                UserName = _configuration["RabbitMq:Username"],
                Password = _configuration["RabbitMq:Password"]
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "alerts", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "alerts", basicProperties: null, body: body);
        }
    }
}
