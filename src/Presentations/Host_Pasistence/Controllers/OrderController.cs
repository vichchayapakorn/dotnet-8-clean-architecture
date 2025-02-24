using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using Application.Orders.Commands;
using Application.Orders.Queries;

namespace Host_Pasistence.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator) { _mediator = mediator; }

        [HttpGet(Name = "GetOrder")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetOrderQuery());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // ตัวอย่าง dummy
            // คุณอาจมี Query Handler แยกสำหรับการ get user
            return Ok(new { Id = id, Username = "JohnDoe", Email = "john@example.com" });
        }

        [HttpPost(Name = "CreateOrder") ]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            // ส่ง command ไปยัง Handler
            var userId = await _mediator.Send(command);
            // ส่งกลับเป็น 201 หรือ 200 ก็ได้
            return CreatedAtAction(nameof(GetById), new { id = userId }, userId);
        }








    }
}
