using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Project.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Webproject.BLL.Services
{
    public  class KafkaConsumer : IHostedService
    {
        private static IConsumer<int, Client> _consumer;

        public KafkaConsumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost",
                GroupId = $"AppName:{Guid.NewGuid().ToString()}",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true,
                ClientId = "2"
            };
            _consumer = new ConsumerBuilder<int, Client>(config)
               .SetValueDeserializer(new MessagePackDeserializer<Client>())
               .Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe("test_Client");
            Task.Factory.StartNew(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var cr = _consumer.Consume(cancellationToken);
                        Console.WriteLine($"Consumed From Kafka: {cr.Message.Value} At: {cr.TopicPartitionOffset} at {cr.Message.Value.DateTime} With Id: {cr.Message.Value.Id}");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error: {e.Error.Reason}");
                    }
                }


            }, cancellationToken);
            return Task.CompletedTask;
        }

            public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
