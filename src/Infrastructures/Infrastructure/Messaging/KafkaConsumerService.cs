using Confluent.Kafka;
using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Interfaces;

namespace Infrastructure.Messaging
{
    public class KafkaConsumerService : IKafkaConsumerService
    {
        private readonly ConsumerConfig _config;

        public KafkaConsumerService()
        {
            _config = new ConsumerConfig
            {
                //BootstrapServers = "localhost:9092",
                BootstrapServers = "10.112.86.164:9092,10.112.86.165:9092,10.112.86.166:9092",
                GroupId = "generic-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false 
            };
        }

        public async Task<List<T>> ConsumeBatchAsync<T>(string topic, string groupId, int batchSize, CancellationToken cancellationToken)
        {
            _config.GroupId = groupId;

            using var consumer = new ConsumerBuilder<string, string>(_config).Build();
            consumer.Subscribe(topic);

            List<ConsumeResult<string, string>> batch = new();
            List<T> messages = new();

            try
            {
                for (int i = 0; i < batchSize; i++)
                {
                    var consumeResult = consumer.Consume(TimeSpan.FromSeconds(10));
                    if (consumeResult == null) break;

                    batch.Add(consumeResult);

                    var message = JsonSerializer.Deserialize<T>(consumeResult.Value);
                    if (message != null)
                    {
                        messages.Add(message);
                    }
                }

                if (messages.Count > 0)
                {
                     //✅ Commit Offset หลังจากอ่านสำเร็จ
                    foreach (var consumeResult in batch)
                    {
                        consumer.Commit(consumeResult);
                    }

                    Console.WriteLine($"✅ Kafka Consumed {messages.Count} messages from {topic}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Kafka Consumer Error: {ex.Message}");
            }

            return messages;
        }
    }
}