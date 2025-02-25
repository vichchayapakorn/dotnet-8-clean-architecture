using Application.Common.Interfaces;
using Domain.Entities;
using Application.Orders.Events;

namespace Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderDbContext _dbContext;
    private readonly IMediator _mediator;

    public CreateOrderCommandHandler(IOrderDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    async Task<int> IRequestHandler<CreateOrderCommand, int>.Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var Order = new Order
        {
            CustomerName = request.CustomerName,
            OrderDate = request.OrderDate,
            TotalAmount = request.TotalAmount,
        };

        _dbContext.Orders.Add(Order);
        await _dbContext.SaveChangesAsync(cancellationToken);


        try
        {
            await _mediator.Publish(new CreateOrderEvent(Order), cancellationToken);
        }
        catch (Exception ex)
        {

        }

        return Order.OrderId;
    }
}



public class CreateOrderCommand : IRequest<int>
{
    public string CustomerName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
}