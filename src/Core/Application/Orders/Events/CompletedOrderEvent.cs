using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Orders.Events
{
    public class CompletedOrderEvent : INotification
    {
        public List<Order> Orders { get; set; }

        public CompletedOrderEvent(List<Order> orders)
        {
            Orders = orders;
        }
    }

    public class CompletedOrderEventHandler : INotificationHandler<CompletedOrderEvent>
    {
       
        private readonly IOrderDbContext _dbContext;

        public CompletedOrderEventHandler(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CompletedOrderEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var order in notification.Orders)
                {
                    var completed = new OrderCompleted
                    {
                        OrderId = order.OrderId,
                        CompletedDate = DateTime.Now,
                        Status = "Complete",
                        //Order = order
                    };
                   _dbContext.OrderCompleteds.Add(completed);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);

                Console.WriteLine($"✅ Saved {notification.Orders.Count} users to database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error saving users to database: {ex.Message}");
            }
        }
    }

}
