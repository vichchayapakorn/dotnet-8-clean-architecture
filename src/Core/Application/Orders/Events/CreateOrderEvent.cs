using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;

namespace Application.Orders.Events
{
    public record CreateOrderEvent(Order OrderEven) : INotification
    {


    }

    public class CreateOrderEventHandler : INotificationHandler<CreateOrderEvent>
    {
        private readonly IKafkaProducerService _kafka;
        public CreateOrderEventHandler (IKafkaProducerService kafka)
        {
            _kafka = kafka;
        }

        public async Task Handle(CreateOrderEvent notification, CancellationToken cancellationToken)
        {
            await _kafka.PublishAsync(notification.OrderEven, notification.OrderEven.OrderDate.Date.ToString());
        }
    }
}
