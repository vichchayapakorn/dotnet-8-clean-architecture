using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Orders.Queries
{
    public class GetOrderQuery : IRequest<List<Order>> 
    { 

    }

    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, List<Order>>
    {
        private readonly IOrderDbContext _context;

        public GetOrderQueryHandler(IOrderDbContext context)
        {
            _context = context;
        }


        public async Task<List<Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var data = await _context.Orders.ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {

            }

            return await _context.Orders.ToListAsync(cancellationToken);
        }
    }


}
