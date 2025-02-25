using Application.Common.Interfaces;
using Domain.Entities;
using DocumentFormat.OpenXml.Spreadsheet;
using Infrastructure.Messaging;
using MediatR;
using Quartz;
using Application.Orders.Events;

namespace BackendQuartz.Jobs
{
    public class OrdertCompletedJob : IJob
    {
        private readonly IMediator _mediator;
        private readonly IKafkaConsumerService _kafkaConsumerService;

        public OrdertCompletedJob(IKafkaConsumerService kafkaConsumerService, IMediator mediator)
        {
            _kafkaConsumerService = kafkaConsumerService;
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var OrderCompleted = await _kafkaConsumerService.ConsumeBatchAsync<Order>("Order-created-topic", "Consume-Order-Group", 50, context.CancellationToken);

            if(OrderCompleted.Count > 0)
            {
                // ✅ Publish Event ให้ MediatR Handle
                await _mediator.Publish(new CompletedOrderEvent(OrderCompleted));
                Console.WriteLine($"✅ Published SaveUsersToDatabaseEvent for {OrderCompleted.Count} users.");
            }
            else
            {
                Console.WriteLine("⚠️ No new messages found.");
            }
        }
    }
}
