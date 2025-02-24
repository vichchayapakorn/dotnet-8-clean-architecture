using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Application.Common.Interfaces;

namespace Infrastructure.Messaging
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IProducer<string, string> _producer;
        private readonly string _topic = "Order-created-topic";

        public KafkaProducerService()
        {
            //var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            var config = new ProducerConfig { BootstrapServers = "10.112.86.164:9092,10.112.86.165:9092,10.112.86.166:9092" };
            
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task PublishAsync<T>(T message, string key)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            await _producer.ProduceAsync(_topic, new Message<string, string>
            {
                Key = key,
                Value = jsonMessage
            });

            Console.WriteLine($"✅ Kafka Message Sent: {jsonMessage}");
        }

    }
}
