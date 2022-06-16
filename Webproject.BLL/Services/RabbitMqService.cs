using MessagePack;
using Project.Models;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;
using Webproject.BLL.Interfaces;

namespace Webproject.BLL.Services
{
    public class RabbitMQService : IRabbitMqService, IDisposable
    {

        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "Test", ExchangeType.Fanout, durable: true);
            _channel.QueueDeclare("client_queue", durable: true, exclusive: false, autoDelete: false);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
        public async Task PublishClientAsync(Client c)
        {

            await Task.Factory.StartNew(() =>
            {

                byte[] bytes = MessagePackSerializer.Serialize(c);
                _channel.BasicPublish(exchange: "", routingKey: "client_queue", body: bytes);
            });

        }

    }
}
