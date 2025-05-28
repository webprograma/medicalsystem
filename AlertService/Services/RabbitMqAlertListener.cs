using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using AlertService.Data;
using AlertService.Models;

namespace AlertService.Services
{
    public class RabbitMqAlertListener : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;

        public RabbitMqAlertListener(IServiceProvider serviceProvider, IConfiguration config)
        {
            _serviceProvider = serviceProvider;
            _config = config;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMq:Host"],
                Port = int.Parse(_config["RabbitMq:Port"] ?? "5672"),
                UserName = _config["RabbitMq:Username"],
                Password = _config["RabbitMq:Password"]
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var queue = _config["RabbitMq:Queue"];

            channel.QueueDeclare(queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.AlertRecords.Add(new AlertRecord { Message = message });
                await db.SaveChangesAsync();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"🚨 [AlertService] Alert received: {message}");
                Console.ResetColor();
            };

            channel.BasicConsume(queue, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
